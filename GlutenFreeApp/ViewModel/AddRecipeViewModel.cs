using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
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
        private string ingridients;
        public string Ingridients 
        {
            get { return ingridients; }
            set { ingridients = value; OnPropertyChanged(); }
        }

        private string instructions;
        public string Instructions
        { 
            get { return instructions; } 
            set {ingridients = value; OnPropertyChanged(); } 
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

        private async void AddRecipe()
        {
            
        }
        #endregion
    }
}
