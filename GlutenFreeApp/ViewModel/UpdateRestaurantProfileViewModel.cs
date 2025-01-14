using GlutenFreeApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlutenFreeApp.ViewModel
{
    public class UpdateRestaurantProfileViewModel:ViewModelBase
    {
        private GlutenFreeServiceWebAPIProxy proxy;
        private IServiceProvider serviceProvider;
        public UpdateRestaurantProfileViewModel(GlutenFreeServiceWebAPIProxy proxy, IServiceProvider serviceProvider)
        {
            this.proxy = proxy;
            this.serviceProvider = serviceProvider;
        }

    }
}
