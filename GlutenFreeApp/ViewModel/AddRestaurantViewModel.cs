using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GlutenFreeApp.Models;
using GlutenFreeApp.Services;

namespace GlutenFreeApp.ViewModel
{
    public class AddRestaurantViewModel:ViewModelBase
    {
        private GlutenFreeServiceWebAPIProxy proxy;
        private IServiceProvider serviceProvider;
        public AddRestaurantViewModel(GlutenFreeServiceWebAPIProxy proxy, IServiceProvider serviceProvider)
        {
            this.proxy = proxy; 
            this.serviceProvider = serviceProvider;
            Submit = new Command(OnSubmit);
            UploadPhotoCommand = new Command(OnUploadPhoto);
            PhotoURL = proxy.GetDefaultRestaurantPhotoUrl();
            LocalPhotoPath = "";
        }

        // all the fields from the register manager to add the restaurant and then will register the restaurant under already logged in manager
        #region properties
        private string restName;
        public string RestName
        {
            get { return restName; }
            set { restName = value; OnPropertyChanged(); }
        }

        private string address;
        public string Address
        {
            get { return address; }
            set { address = value; OnPropertyChanged(); }

        }

        private int typeFood;
        public int TypeFood
        {
            get { return typeFood; }
            set { typeFood = value; OnPropertyChanged(); }
        }
        #endregion

        #region submit
        public ICommand Submit { set; get; }
        private async void OnSubmit()
        {
            //create the restaurant
            var newRest = new RestaurantInfo
            {
                RestAddress = this.Address,
                StatusID = 2,//PENDING
                TypeFoodID = this.TypeFood + 1,
                UserID = (int)((App)Application.Current).LoggedInUser.UserID,
                RestName = this.RestName
            };
            
            InServerCall = true;
            newRest = await this.proxy.AddRestaurant(newRest);
            InServerCall = false;
            if (newRest != null)
            {
                //if the restaurant without the image was successfuly uploaded-> we will upload the image as well
                //this means the select photo method only gets the image path and doesnt upload it yet
                Restaurantid = newRest.RestID;
                RestaurantInfo? updatedrestaurant = await proxy.UploadRestaurantImage(LocalPhotoPath, Restaurantid);
                await Application.Current.MainPage.DisplayAlert("Restaurant Is Up And Waiting For Approval", "Success", "ok");
                //restart the properties
                Address = "";
                RestName = "";
                TypeFood = 0;
                PhotoURL = proxy.GetDefaultRestaurantPhotoUrl();
            }
            else
                await Application.Current.MainPage.DisplayAlert("Something Went Wrong", "nah", "ok");
        }
        #endregion


        #region Photo
        //this holds the restaurant id after the user uploaded it so we can sort it in the files
        private int restaurantid;
        public int Restaurantid
        {
            get => restaurantid;
            set
            {
                if (restaurantid != value)
                {
                    restaurantid = value;
                    OnPropertyChanged();
                }
            }
        }

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
        //This method open the file picker to select a photo and saves the photo but doesnt upload it yet
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
    }
}
