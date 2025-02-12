using GlutenFreeApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GlutenFreeApp.Services;

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

        }
        #endregion

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

        private bool isSterile;
        public bool IsSterile
        {
            get { return isSterile; }
            set { isSterile = value; OnPropertyChanged(); }
        }

        private bool isNotSterile;
        public bool IsNotSterile
        {
            get { return isNotSterile; }
            set { isNotSterile = value; OnPropertyChanged(); }
        }

        public ICommand Submit {  get; set; }

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

        private RestaurantInfo restaurantSelected;
        public RestaurantInfo RestaurantSelected
        {
            get => restaurantSelected;
            set
            {
                if (value != restaurantSelected)
                {
                    restaurantSelected = value;

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
