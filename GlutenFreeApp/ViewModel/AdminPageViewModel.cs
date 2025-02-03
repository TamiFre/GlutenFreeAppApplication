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
       //cant accept or approve unless pending is selected

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
            errorMsgStatusRest = "";
            errorMsgStatusRecipe = "";
            AddFactCommand = new Command(AddFact);
            FillAllRestaurants();
            FillAllRecipes();
            SearchRestByStatus = new Command(ShowRestaurantsByStatus);
            SearchRecipeByStatus = new Command(ShowRecipesByStatus);
            ApproveRestaurant = new Command(ChangeRestStatusToApprove);
            DeclineRestaurant = new Command(ChangeRestStatusToDecline);
            DeclineRecipe = new Command(ChangeRecipeStatusToDecline);
            ApproveRecipe = new Command(ChangeRecipeStatusToApprove);
            
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
            if (StatusRestSelected == 0)
            {
                List<RestaurantInfo> restaurants = new List<RestaurantInfo>();
                restaurants = await GetAllRestaurants();
                return restaurants;
            }
            List<RestaurantInfo> list = await this.proxy.GetRestaurantByStatus(StatusRestSelected);
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
            if (StatusRecipeSelected  == 0)
            {
                List<RecipeInfo> recipes = new List<RecipeInfo>();
                recipes = await GetAllRecipes();
               return recipes;
            }
            else 
            {
                List<RecipeInfo> list = await this.proxy.GetRecipetByStatus(StatusRecipeSelected );
                return list; 
            }
            
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

        private string errorMsgStatusRest;
        public string ErrorMsgStatusRest
        {
            get => errorMsgStatusRest;
            set
            {
                if (errorMsgStatusRest != value)
                {
                    errorMsgStatusRest = value;
                    OnPropertyChanged(nameof(ErrorMsgStatusRest));
                }
            }
        }

        private string errorMsgStatusRecipe;
        public string ErrorMsgStatusRecipe
        {
            get => errorMsgStatusRecipe;
            set 
            {
                if (errorMsgStatusRecipe != value)
                {
                    errorMsgStatusRecipe = value;
                    OnPropertyChanged(nameof(ErrorMsgStatusRecipe));
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
        #endregion

        #region Restaurant selected - approve and decline
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
            ErrorMsgStatusRest = "";
            bool success = await this.proxy.ChangeRestStatusToApproved(SelectedRest);
            if (success)
            {
                ErrorMsgStatusRest = "Status Changed to Approved";
            }
            else
                ErrorMsgStatusRest = "Something Went Wrong";
            ShowRestaurantsByStatus();
        }

        public async void ChangeRestStatusToDecline()
        {
            ErrorMsgStatusRest = "";
            bool success = await this.proxy.ChangeRestStatusToDecline(SelectedRest);
            if (success)
            {
                ErrorMsgStatusRest = "Status Changed To Declined";
            }
            else
              ErrorMsgStatusRest = "Something went Wrong";
            ShowRestaurantsByStatus();
        }
        #endregion

        #region Recipe Selected - approve and decline 
        private RecipeInfo objetRecipeSelected;
        public RecipeInfo ObjetRecipeSelected
        {
            get => objetRecipeSelected;
            set 
            {
                objetRecipeSelected = value;
                if (value != null)
                {
                    // Extract the Id property by from the restaurant object
                    int id = value.RecipeID;
                    SelectedRecipe = RecipesCol.Where(r => r.RecipeID == id).FirstOrDefault();
                }
                else
                    SelectedRecipe = null;
                OnPropertyChanged();
            }
        }

        private RecipeInfo selectedRecipe;
        public RecipeInfo SelectedRecipe
        {
            get => selectedRecipe;
            set 
            {
                if (value != null)
                {
                    selectedRecipe = value;
                    OnPropertyChanged();
                }
            }
        }

        public async void ChangeRecipeStatusToApprove()
        {
            ErrorMsgStatusRecipe = "";
            bool success = await this.proxy.ChangeRecipeStatusToApprove(SelectedRecipe);
            if (success)
            {
                ErrorMsgStatusRecipe = "Status Changed to Approved";
            }
            else
                ErrorMsgStatusRecipe = "Something Went Wrong";
            ShowRecipesByStatus();
        }

        public async void ChangeRecipeStatusToDecline()
        {
            ErrorMsgStatusRecipe = "";
            bool success = await this.proxy.ChangeRecipeStatusToDecline(SelectedRecipe);
            if (success)
            {
                ErrorMsgStatusRecipe = "Status Changed to Decline";
            }
            else
                ErrorMsgStatusRecipe = "Something Went Wrong";
            ShowRecipesByStatus();
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
