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

            //ASK OFER _ HOW RO ACCESS THE SINGULAR ITEM AND FOOD TYPE

            //ExpandCommand - new Command(FillAllCritics());
        }

        #region observable collection
        private ObservableCollection<RestaurantInfo> restaurantsCol;
        public ObservableCollection<RestaurantInfo> RestaurantsCol { get { return restaurantsCol; } set { restaurantsCol = value; OnPropertyChanged(); } }
        #endregion

        #region observable collection of the critics of the chosen restaurant
        private ObservableCollection<CriticInfo> criticCol;
        public ObservableCollection<CriticInfo> CriticCol
        {
            get { return criticCol; }
            set { criticCol = value; OnPropertyChanged(); }
        }
        #endregion

        #region get critics
        public ICommand ExpandCommand { get; set; }

        public async void FillAllCritics(RestaurantInfo restaurantInfo)
        {
            List<CriticInfo> critics = new List<CriticInfo>();
            critics = await this.proxy.GetCriticForRestaurant(restaurantInfo);
            CriticCol = new ObservableCollection<CriticInfo>(critics);
        }
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

        private string stringFoodSelected;
        public string StringFoodSelected
        {
            get => stringFoodSelected;
            set
            {
                stringFoodSelected = value;
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
            if (TypeFoodSelected + 1 == 1)
            {
                List<RestaurantInfo> list1 = await this.proxy.GetAllApprovedRestaurants();
                return list1;
            }
            else 
            { 
            List<RestaurantInfo> list = await this.proxy.GetApprovedRestaurantsByChosenFoodType(TypeFoodSelected + 1);
            return list;
            }
        }

        public ICommand SearchByFoodSelected { get; set; }
        #endregion

    }
}
