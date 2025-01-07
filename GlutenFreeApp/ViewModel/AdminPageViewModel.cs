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
        private ObservableCollection<RecipeInfo> recipesCol;
        public ObservableCollection<RecipeInfo> RecipesCol { get { return recipesCol; } set { recipesCol = value;OnPropertyChanged(); } }
        private ObservableCollection<RestaurantInfo> restaurantsCol;
        public ObservableCollection<RestaurantInfo> RestaurantsCol { get { return restaurantsCol; } set { restaurantsCol = value; OnPropertyChanged(); } }
  
        #endregion

        private GlutenFreeServiceWebAPIProxy proxy;
        private IServiceProvider serviceProvider;

        #region builder
        public AdminPageViewModel(GlutenFreeServiceWebAPIProxy proxy, IServiceProvider serviceProvider) 
        {
            this.serviceProvider = serviceProvider;
            this.proxy = proxy;
            errorMsg = "";
            errorMsgStatus = "";
            AddFactCommand = new Command(AddFact);
            FillAllRestaurants();
            FillAllRecipes();
            SearchRestByStatus = new Command(ShowRestaurantsByStatus);
            SearchRecipeByStatus = new Command(ShowRecipesByStatus);
            ApproveRestaurant = new Command(ChangeRestStatusToApprove);
        }
        #endregion

        #region show restaurants by status chosen in the picker
        public async void ShowRestaurantsByStatus()
        {
            //get the filtered list
            List<RestaurantInfo> restaurantsByStatus = new List<RestaurantInfo>();
            restaurantsByStatus = await GetAllRestaurantsByStatus();
            RestaurantsCol.Clear();
            RestaurantsCol = new ObservableCollection<RestaurantInfo>(restaurantsByStatus);
        }

        public async Task<List<RestaurantInfo>> GetAllRestaurantsByStatus()
        {
            List<RestaurantInfo> list = await this.proxy.GetRestaurantByStatus(StatusRestSelected+1);
            return list;
        }
        #endregion
        
        #region show recipes by status chosen in the picker
        public async void ShowRecipesByStatus()
        {
            //get the filtered list
            List<RecipeInfo> recipesByStatus = new List<RecipeInfo>();
            recipesByStatus = await GetAllRecipesByStatus();
            RecipesCol.Clear();
            RecipesCol = new ObservableCollection<RecipeInfo>(recipesByStatus);
        }

        public async Task<List<RecipeInfo>> GetAllRecipesByStatus()
        {
            List<RecipeInfo> list = await this.proxy.GetRecipetByStatus(StatusRecipeSelected + 1);
            return list;
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

        private string errorMsgStatus;
        public string ErrorMsgStatus
        {
            get => errorMsgStatus;
            set
            {
                if (errorMsgStatus != value)
                {
                    errorMsgStatus = value;
                    OnPropertyChanged(nameof(ErrorMsgStatus));
                }
            }
        }

        private int statusRestSelected;
        public int StatusRestSelected
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

        private int statusRecipeSelected;
        public int StatusRecipeSelected
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
        public ICommand SearchRestByStatus { get; set; }
        public ICommand SearchRecipeByStatus { get; set; }
        public ICommand AddFactCommand { get; set; }
        public ICommand ApproveRestaurant { get; set; }
        public ICommand DeclineRestaurant { get; set; }
        public ICommand ApproveRecipe { get; set; }
        public ICommand DeclineRecipe { get; set; }


        private RestaurantInfo selectedObject;
        public RestaurantInfo SelectedObject
        {
            get => selectedObject;
            set
            {
                selectedObject = value;
                if (value != null)
                {
                    // Extract the Id property by from the restaurant object
                    int id = value.RestID;
                    SelectedRest = RestaurantsCol.Where(r => r.RestID == id).FirstOrDefault();
                }
                else
                    SelectedRest = null;
                OnPropertyChanged();
            }
        }

        private RestaurantInfo selesctedRest;
        public RestaurantInfo SelectedRest
        {
            get => selesctedRest;
            set
            {
                if (value != null)
                {
                    selesctedRest = value;
                    OnPropertyChanged();
                }

            }
        }

        public async void ChangeRestStatusToApprove()
        {
            bool success = await this.proxy.ChangeRestStatusToApproved(SelectedRest);
            if (success)
            {
                ErrorMsgStatus = "Status Changed to Approved";
            }
            ErrorMsgStatus = "Something Went Wrong";
        }
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
            RestaurantsCol = new ObservableCollection<RestaurantInfo>(restaurants);    
        }

        public async Task<List<RestaurantInfo>> GetAllRestaurants()
        {
            List<RestaurantInfo> list = await this.proxy.GetAllRestaurants();
            return list;
        }
        #endregion

        #region get all recipes
        //fill the observable collection with the data
        public async void FillAllRecipes()
        {
            List<RecipeInfo> recipes = new List<RecipeInfo>();
            recipes = await GetAllRecipes();
            RecipesCol = new ObservableCollection<RecipeInfo>(recipes);
        }
        public async Task<List<RecipeInfo>> GetAllRecipes()
        {
            List<RecipeInfo> list = await this.proxy.GetAllRecipes();
            return list;
        }
        #endregion
    }
}
