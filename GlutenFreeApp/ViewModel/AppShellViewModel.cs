using GlutenFreeApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GlutenFreeApp.Views;

namespace GlutenFreeApp.ViewModel
{
    public class AppShellViewModel:ViewModelBase
    {
        private UsersInfo? currentUser;
        private IServiceProvider serviceProvider;
        public AppShellViewModel(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
            this.currentUser = ((App)Application.Current).LoggedInUser;
        }

        //check if someone is logged in - returns true if someone is NOT logged in
        public bool IsLoggedIn
        {
            get 
            {
                return this.currentUser==null;
            }
        }

        //check if its admin
        public bool IsManager
        {
            get
            {
                return (this.currentUser.TypeID==2);
            }
        }

        //check if its a restaurant owner
        public bool IsRestaurantManager
        {
            get 
            {
                return (this.currentUser.TypeID == 3);
            }
        }

        //this command will be used for logout menu item
        public Command LogoutCommand
        {
            get
            {
                return new Command(OnLogout);
            }
        }
        //this method will be trigger upon Logout button click
        public void OnLogout()
        {
            ((App)Application.Current).LoggedInUser = null;

            ((App)Application.Current).MainPage = new NavigationPage(serviceProvider.GetService<LoginPageView>());
        }
    }
}
