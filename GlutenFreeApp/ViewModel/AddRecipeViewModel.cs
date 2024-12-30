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
    public class AddRecipeViewModel:ViewModelBase
    {
        private GlutenFreeServiceWebAPIProxy proxy;
        private IServiceProvider serviceProvider;

        #region builder
        public AddRecipeViewModel(GlutenFreeServiceWebAPIProxy proxy, IServiceProvider serviceProvider)
        {
            this.proxy = proxy;
            this.serviceProvider = serviceProvider;
            SubmitRecipeCommand = new Command(AddRecipe);
        }
        #endregion

        #region Properties
        private string recipe;
        public string Recipe
        {
            get { return recipe; }
            set { recipe = value; OnPropertyChanged(); }
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

        private string typeFood;
        public string TypeFood
        {
            get { return typeFood; }
            set { typeFood = value; OnPropertyChanged(); }
        }
        #endregion

        #region buttons
        public ICommand SubmitRecipeCommand { get; set; }
        #endregion

        //Check 

        private async void AddRecipe()
        {
            InServerCall = true;

            UsersInfo? u = ((App)Application.Current).LoggedInUser;
            //call the server to add the information

            //add the recipe as pending

            //how to add user id 
            RecipeInfo information = new RecipeInfo { RecipeText = Recipe, StatusID=2, UserID = u.UserID.Value };
            bool worked = await this.proxy.AddRecipeAsync(information);
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
       
    }
}
