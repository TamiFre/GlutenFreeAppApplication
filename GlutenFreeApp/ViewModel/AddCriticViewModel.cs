using GlutenFreeApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GlutenFreeApp.Services;
using Microsoft.Identity.Client;

namespace GlutenFreeApp.ViewModel
{
    public class AddCriticViewModel:ViewModelBase
    {
        private GlutenFreeServiceWebAPIProxy proxy;
        private IServiceProvider serviceProvider;

        #region builder
        public AddCriticViewModel(GlutenFreeServiceWebAPIProxy proxy, IServiceProvider serviceProvider)
        {
            this.proxy = proxy;
            this.serviceProvider = serviceProvider;
            Fill();
            Submit = new Command(OnSubmit);
            UploadPhotoCommand = new Command(OnUploadPhoto);
            PhotoURL = proxy.GetDefaultRecipePhotoUrl();
            LocalPhotoPath = "";
        }
        #endregion
        #region properties
        private string restName;
        public string RestName
        {
            get { return restName; }
            set { restName = value; OnPropertyChanged(); }
        }

        private string theCritic;
        public string TheCritic
        {
            get { return theCritic; }
            set { theCritic = value; OnPropertyChanged(); }
        }
        #endregion
        public ICommand Submit {  get; set; }

        #region Photo

        private string photoURL;

        public string PhotoURL
        {
            get => photoURL;
            set
            {
                photoURL = value;
                OnPropertyChanged("PhotoURL");
            }
        }

        private string localPhotoPath;

        public string LocalPhotoPath
        {
            get => localPhotoPath;
            set
            {
                localPhotoPath = value;
                OnPropertyChanged("LocalPhotoPath");
            }
        }

        public Command UploadPhotoCommand { get; }
        //This method open the file picker to select a photo
        private async void OnUploadPhoto()
        {
            try
            {
                var result = await MediaPicker.Default.CapturePhotoAsync(new MediaPickerOptions
                {
                    Title = "Please select a photo",
                });

                if (result != null)
                {
                    // The user picked a file
                    this.LocalPhotoPath = result.FullPath;
                    this.PhotoURL = result.FullPath;
                }


            }
            catch (Exception ex)
            {
            }

        }
        #endregion

        public async void OnSubmit()
        {
            InServerCall = true;
            UsersInfo? u = ((App)Application.Current).LoggedInUser;
            CriticInfo? criticInfo = new CriticInfo()
            {
                CriticText = this.TheCritic,
                UserID = u.UserID,
                RestID = this.RestaurantSelectedID
            };
            CriticInfo worked = await this.proxy.AddCritic(criticInfo);
            if (worked == null)
            {
                await Application.Current.MainPage.DisplayAlert("Something Went Wrong", "nah", "ok");
            }
            else
            {
                int criticid = worked.CriticID;
                InServerCall = false;
                CriticInfo? updatedcritic = await proxy.UploadCriticImage(LocalPhotoPath, criticid);
                // restart the properties
                worked.ProfileImagePath = updatedcritic.ProfileImagePath;
                TheCritic = "";

                await Application.Current.MainPage.DisplayAlert("Critic was", "Uploaded", "ok");
            }
        }

        #region pICKER
        private ObservableCollection<RestaurantInfo> restaurants;
        public ObservableCollection<RestaurantInfo> Restaurants
        {
            get => restaurants;
            set
            {
                if (value != restaurants)
                {
                    restaurants = value;
                    OnPropertyChanged();
                }
            }
        }

        private int restaurantSelectedID;
        public int RestaurantSelectedID
        {
            get => restaurantSelectedID;
            set 
            {
                if (value != restaurantSelectedID)
                {
                    restaurantSelectedID = value; 
                    OnPropertyChanged();
                }
            }
        }

        private RestaurantInfo restaurantSelected;
        public RestaurantInfo RestaurantSelected
        {
            get => restaurantSelected;
            set
            {
                if (value != restaurantSelected)
                {
                    restaurantSelected = value;
                    RestaurantSelectedID = this.RestaurantSelected.RestID;
                    OnPropertyChanged();
                }
            }
        }

        private async void Fill()
        {
            List<RestaurantInfo> restaurantslist = await this.proxy.GetAllApprovedRestaurants();
            //add error msg if null
            Restaurants = new ObservableCollection<RestaurantInfo>(restaurantslist);
        }
        #endregion  


    }
}
