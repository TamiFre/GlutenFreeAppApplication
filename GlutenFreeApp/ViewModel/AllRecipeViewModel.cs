﻿using GlutenFreeApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GlutenFreeApp.Models;
using GlutenFreeApp.Views;
using System.Windows.Input;

namespace GlutenFreeApp.ViewModel
{
    public class AllRecipeViewModel:ViewModelBase
    {
        private GlutenFreeServiceWebAPIProxy proxy;
        private IServiceProvider serviceProvider;

    
        #region  Food Types
        //the statuses and food types
        private ObservableCollection<TypeFoodInfo> foodTypeList;
        public ObservableCollection<TypeFoodInfo> FoodTypeList
        {
            get => foodTypeList;
            set
            {
                foodTypeList = value;
                OnPropertyChanged();
            }
        }
        #endregion
        public AllRecipeViewModel(GlutenFreeServiceWebAPIProxy proxy, IServiceProvider serviceProvider)
        {
            this.proxy = proxy;
            this.serviceProvider = serviceProvider;
            FillAllRecipes();
            SearchByFoodTypeSelected = new Command(ShowRecipesByFoodSelected);
            //get the data from the app - how to access it?
            FoodTypeList = new ObservableCollection<TypeFoodInfo>(((App)Application.Current).FoodTypes);
            ExpandCommand = new Command(OnExpandCommand);
            CloseCommand = new Command(OnCloseCommand);
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
            if (TypeFoodSelected == 0)
            {
                List<RecipeInfo> list1 = await this.proxy.GetAllApprovedRecipes();
                return list1;
            }
            else
            {
                List<RecipeInfo> list = await this.proxy.GetApprovedRecipesByChosenFoodType(TypeFoodSelected );
                return list;
            }
        }
        #endregion

        #region Properties

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

        #region expanbd the recipe
        public ICommand ExpandCommand { set; get; }
        public async void ShowFullRecipe(RecipeInfo recipeInfo)
        {
            var popupPage = new PopupPageRecipesView(this);
            ((App)(Application.Current)).MainPage.Navigation.PushAsync(popupPage);
        }

        private async void OnExpandCommand()
        {
            UpdateFullRecipe();
            ShowFullRecipe(ObjetRecipeSelected);
        }

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

        private string selectedRecipeFullRecipe;
        public string SelectedRecipeFullRecipe
        {
            get => selectedRecipeFullRecipe;
            set
            {
                if (selectedRecipeFullRecipe != value)
                {
                    selectedRecipeFullRecipe = value;
                    OnPropertyChanged();
                }
            }
            
        }
        private async void UpdateFullRecipe()
        {
            SelectedRecipeFullRecipe = ObjetRecipeSelected.RecipeText;
        }


        public ICommand CloseCommand { get; set; }
        public async void OnCloseCommand()
        {
            ((App)(Application.Current)).MainPage.Navigation.PopAsync();
        }


        #endregion

    }
}
