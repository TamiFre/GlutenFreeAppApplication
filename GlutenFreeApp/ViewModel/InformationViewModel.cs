using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GlutenFreeApp.Services;

namespace GlutenFreeApp.ViewModel
{
    public class InformationViewModel:ViewModelBase
    {
        private GlutenFreeServiceWebAPIProxy proxy;
        private IServiceProvider serviceProvider;
        #region builder
        public InformationViewModel(GlutenFreeServiceWebAPIProxy proxy, IServiceProvider serviceProvider) 
        {
            this.proxy = proxy;
            this.serviceProvider = serviceProvider;
        }
        #endregion
    }
}
