using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GlutenFreeApp.Models;
using GlutenFreeApp.Services;



namespace GlutenFreeApp.ViewModel
{
    public class AdminPageViewModel:ViewModelBase
    {
        //to do:
        //do a command that adds the fact the admin wants to add to the DB - done
        //do a sort of table that shows all of the pending restaurants and allow the admin to approve them
        //order: first the facts and then do the table - example might be in talsi's git with the monkeys

        #region collection view 
        //private ObservableCollection<RecipeInfo> recipesCol;
        public ObservableCollection<RecipeInfo> Recipes { get; set; }
        //private ObservableCollection<RestaurantInfo> restaurantsCol;
        public ObservableCollection<RestaurantInfo> Restaurants { get; set; }
  
        #endregion

        private GlutenFreeServiceWebAPIProxy proxy;
        private IServiceProvider serviceProvider;

        #region builder
        public AdminPageViewModel(GlutenFreeServiceWebAPIProxy proxy, IServiceProvider serviceProvider) 
        {
            this.serviceProvider = serviceProvider;
            this.proxy = proxy;
            errorMsg = "";
            AddFactCommand = new Command(AddFact);
            FillAllRestaurants();
        }
        #endregion

        #region properties
        private string fact;
        public string Fact 
        {
            get => fact;
            set 
            {
                if (fact!=value)
                {
                    fact = value;
                    OnPropertyChanged();
                }
            }
        }

        private string errorMsg;
        public string ErrorMsg
        {
            get => errorMsg;
            set
            {
                if (errorMsg != value)
                {
                    errorMsg = value;
                    OnPropertyChanged(nameof(ErrorMsg));
                }
            }
        }

        private string statusRestSelected;
        public string StatusRestSelected
        {
            get => statusRestSelected;
            set 
            {
                if (statusRestSelected != value)
                {
                    statusRestSelected = value;
                    OnPropertyChanged(nameof(StatusRestSelected));
                }
            }
        }

        private string statusRecipeSelected;
        public string StatusRecipeSelected
        {
            get => statusRecipeSelected;
            set
            {
                if (statusRecipeSelected != value)
                {
                    statusRecipeSelected = value;
                    OnPropertyChanged(nameof(StatusRestSelected));
                }
            }
        }
        #endregion

        #region buttons
        public ICommand AddFactCommand { get; set; }
        public ICommand GetRestByStatus { get; set; }
        public ICommand GetRecipeByStatus { get; set; }
        #endregion

        #region add fact
        private async void AddFact()
        {
            ErrorMsg = "";
            InServerCall = true;

            //call the server to add the information

            InformationInfo information = new InformationInfo { InfoText = Fact};
            bool worked = await this.proxy.AddFactAsync(information);
            InServerCall = false;

            if (!worked)
            {
                ErrorMsg = "Something Went Wrong";
            }
            else 
            {
                ErrorMsg = "All Good";
            }
            
           
        }
        #endregion

        #region get all restaurants
        // fill the observable collection with all the restaurants
        public async void FillAllRestaurants()
        {
            List<RestaurantInfo> restaurants = new List<RestaurantInfo>();
            restaurants = await GetAllRestaurants();
            Restaurants = new ObservableCollection<RestaurantInfo>(restaurants);
        }

        public async Task<List<RestaurantInfo>> GetAllRestaurants()
        {
            List<RestaurantInfo> list = await this.proxy.GetAllRestaurants();
            return list;
        }
        #endregion


    }
}
