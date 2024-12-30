using GlutenFreeApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GlutenFreeApp.Models;

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

        #region Properties
        //maybe do it an int and do a constant to each typefood because of the dto and models

        private string typeFoodSelected;
        public string TypeFoodSelected 
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
