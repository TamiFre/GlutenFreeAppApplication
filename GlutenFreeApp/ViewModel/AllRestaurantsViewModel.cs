using System;
using GlutenFreeApp.Services;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using GlutenFreeApp.Models;
using System.Windows.Input;
namespace GlutenFreeApp.ViewModel
{
    public class AllRestaurantsViewModel:ViewModelBase
    {
        private GlutenFreeServiceWebAPIProxy proxy;
        private IServiceProvider serviceProvider;

        public AllRestaurantsViewModel(GlutenFreeServiceWebAPIProxy proxy, IServiceProvider serviceProvider)
        {
            this.proxy = proxy;
            this.serviceProvider = serviceProvider;
            FillAllApprovedRestaurants();
            SearchByFoodSelected = new Command(ShowRestaurantsByFoodSelected);
        }

        #region observable collection
        private ObservableCollection<RestaurantInfo> restaurantsCol;
        public ObservableCollection<RestaurantInfo> RestaurantsCol { get { return restaurantsCol; } set { restaurantsCol = value; OnPropertyChanged(); } }
        #endregion

        #region get approved restauratns
        public async void FillAllApprovedRestaurants()
        {
            List<RestaurantInfo> restaurants = new List<RestaurantInfo>();
            restaurants = await GetAllApprovedRestaurants();
            RestaurantsCol = new ObservableCollection<RestaurantInfo>(restaurants);
        }
        public async Task<List<RestaurantInfo>> GetAllApprovedRestaurants()
        {
            List<RestaurantInfo> list = await this.proxy.GetAllApprovedRestaurants();
            return list;
        }
        #endregion

        #region Properties
        //maybe do it an int and do a constant to each typefood because of the dto and models

        private int typeFoodSelected;
        public int TypeFoodSelected
        {
            get => typeFoodSelected;
            set
            {
                typeFoodSelected = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region show restaurants by food type selected
        public async void ShowRestaurantsByFoodSelected()
        {
            //get the filtered list
            List<RestaurantInfo> restaurantsByStatus = new List<RestaurantInfo>();
            restaurantsByStatus = await GetAllRestaurantsByFoodSelected();
            RestaurantsCol.Clear();
            RestaurantsCol = new ObservableCollection<RestaurantInfo>(restaurantsByStatus);
        }

        public async Task<List<RestaurantInfo>> GetAllRestaurantsByFoodSelected()
        {
            List<RestaurantInfo> list = await this.proxy.GetApprovedRestaurantsByChosenFoodType(TypeFoodSelected + 1);
            return list;
        }

        public ICommand SearchByFoodSelected { get; set; }
        #endregion

        //not working
        #region transform the int of the food type to a string
        public enum FoodType
        {
            Italian = 1,
            Asian = 2,
            Mexican = 3,
            BBQ = 4,
            French = 5
        }

       
        #endregion

    }
}
