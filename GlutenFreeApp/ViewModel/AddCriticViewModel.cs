using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GlutenFreeApp.ViewModel
{
    public class AddCriticViewModel:ViewModelBase
    {
        private string restName;
        public string RestName
        {
            get { return restName; }
            set { restName = value; OnPropertyChanged(); }
        }

        private string theCritic;
        public string TheCritic
        {
            get { return theCritic; }
            set { theCritic = value; OnPropertyChanged(); }
        }

        private bool isSterile;
        public bool IsSterile
        {
            get { return isSterile; }
            set { isSterile = value; OnPropertyChanged(); }
        }

        private bool isNotSterile;
        public bool IsNotSterile
        {
            get { return isNotSterile; }
            set { isNotSterile = value; OnPropertyChanged(); }
        }

        public ICommand Submit {  get; set; }   


    }
}
