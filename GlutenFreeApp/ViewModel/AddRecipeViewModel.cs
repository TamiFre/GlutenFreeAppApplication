using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlutenFreeApp.ViewModel
{
    public class AddRecipeViewModel:ViewModelBase
    {
        private string? ingridients;
        public string? Ingridients 
        {
            get { return ingridients; }
            set { ingridients = value; OnPropertyChanged(); }
        }

       

    }
}
