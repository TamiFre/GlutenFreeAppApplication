using GlutenFreeApp.Models;
using GlutenFreeApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GlutenFreeApp.ViewModel
{
    public class UpdateRestaurantProfileViewModel:ViewModelBase
    {
        private GlutenFreeServiceWebAPIProxy proxy;
        private IServiceProvider serviceProvider;
        public UpdateRestaurantProfileViewModel(GlutenFreeServiceWebAPIProxy proxy, IServiceProvider serviceProvider)
        {
            this.proxy = proxy;
            this.serviceProvider = serviceProvider;
            Fill();
            OnSubmit = new Command(OnSubmitCommand); 
        }

        #region properties
        private string restAddress;
        public string RestAddress 
        {
            get => restAddress;
            set
            {
                if (restAddress != value)
                {
                    restAddress = value;
                    OnPropertyChanged();
                }
            }
        }

        private string restName;
        public string RestName
        {
            get => restName;
            set
            {
                if (restName != value)
                {
                    restName = value;
                    OnPropertyChanged();
                }
            }
        }
        #endregion

        #region picker
        private ObservableCollection<RestaurantInfo> userRestaurants;
        public ObservableCollection<RestaurantInfo> UserRestaurants
        {
            get => userRestaurants;
            set 
            {
                if(value!=userRestaurants)
                {
                    userRestaurants = value;
                    OnPropertyChanged();
                }
            }
        }

        private RestaurantInfo restSelected;
        public RestaurantInfo RestSelected
        {
            get => restSelected;
            set 
            {
                if (value != restSelected)
                {
                    restSelected = value;
                    OnPropertyChanged();
                }
            }
        }

        private async void Fill()
        {
            UsersInfo currentUser = ((App)(Application.Current)).LoggedInUser;
            int id = 0;
            if (currentUser == null)
            {
                Error = "user not logged in";
                
            }
            id = (int)((App)(Application.Current)).LoggedInUser.UserID;
            List<RestaurantInfo> list = await this.proxy.GetAllUserRestaurants(id);
            UserRestaurants = new ObservableCollection<RestaurantInfo>(list);
            Error = "";
            
        }

        private string error;
        public string Error 
        {
            get => error;
            set 
            {
                if (value != error)
                {
                    error = value;
                    OnPropertyChanged();
                }
            }
        }
        #endregion

        //ask ofer
        #region submit
        public ICommand OnSubmit { set; get; }

        private async void OnSubmitCommand()
        {
            RestaurantInfo current = RestSelected;
            current.RestName = this.RestName;
            current.RestAddress = this.RestAddress;
            InServerCall = true;
            bool success = await proxy.UpdateRestaurant(current);
            if (success)
            {
                await Application.Current.MainPage.DisplayAlert("Profile Updated", "Success", "ok");
                  
            }

        }
        #endregion

    }
}
