using GlutenFreeApp.Views;
using GlutenFreeApp.ViewModel;
using GlutenFreeApp.Models;

namespace GlutenFreeApp
{
    public partial class App : Application
    {

        public UsersInfo? LoggedInUser { get; set; }
        public App(IServiceProvider serviceProvider)
        {

            InitializeComponent();
            LoginPageView? v = serviceProvider.GetService<LoginPageView>();
            MainPage = new NavigationPage(v);
        }
    }
}
