using GlutenFreeApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GlutenFreeApp.Models;
using System.Windows.Input;

namespace GlutenFreeApp.ViewModel
{
    public class AllRecipeViewModel:ViewModelBase
    {
        private GlutenFreeServiceWebAPIProxy proxy;
        private IServiceProvider serviceProvider;

        // the page shows the approved recipes. need to also show the food type of the recipe and do a picker to search the recipes by food type
        public AllRecipeViewModel(GlutenFreeServiceWebAPIProxy proxy, IServiceProvider serviceProvider)
        {
            this.proxy = proxy;
            this.serviceProvider = serviceProvider;
            FillAllRecipes();
            SearchByFoodTypeSelected = new Command(ShowRecipesByFoodSelected);
        }

        #region Observsble collection

        private ObservableCollection<RecipeInfo> recipesCol;
        public ObservableCollection<RecipeInfo> RecipesCol { get { return recipesCol; } set { recipesCol = value; OnPropertyChanged(); } }
        #endregion

        #region Fill The Approved Recipes
        public async void FillAllRecipes()
        {
            List<RecipeInfo> recipes = new List<RecipeInfo>();
            recipes = await GetAllRecipes();
            RecipesCol = new ObservableCollection<RecipeInfo>(recipes);
        }
        public async Task<List<RecipeInfo>> GetAllRecipes()
        {
            List<RecipeInfo> list = await this.proxy.GetAllApprovedRecipes();
            return list;
        }
        #endregion

        #region Show By Food Selection
        public ICommand SearchByFoodTypeSelected { get; set; }
        public async void ShowRecipesByFoodSelected()
        {

            //get the filtered list
            List<RecipeInfo> recipesByStatus = new List<RecipeInfo>();
            recipesByStatus = await GetAllRecipesByFoodSelected();
            RecipesCol.Clear();
            RecipesCol = new ObservableCollection<RecipeInfo>(recipesByStatus);
        }

        public async Task<List<RecipeInfo>> GetAllRecipesByFoodSelected()
        {
            if (TypeFoodSelected + 1 == 1)
            {
                List<RecipeInfo> list1 = await this.proxy.GetAllApprovedRecipes();
                return list1;
            }
            else
            {
                List<RecipeInfo> list = await this.proxy.GetApprovedRecipesByChosenFoodType(TypeFoodSelected + 1);
                return list;
            }
        }
        #endregion

        #region Properties
        //maybe do it an int and do a constant to each typefood because of the dto and models

        private int typeFoodSelected;
        public int TypeFoodSelected 
        {
            get=> typeFoodSelected;
            set
            {
                typeFoodSelected = value; 
                OnPropertyChanged();
            }
        }
        #endregion

    }
}
