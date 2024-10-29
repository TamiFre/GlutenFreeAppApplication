using GlutenFreeApp.Views;
using GlutenFreeApp.ViewModel;

namespace GlutenFreeApp
{
    public partial class App : Application
    {
        public App(LoginPageViewModel vm)
        {
            InitializeComponent();

            MainPage = new LoginPageView(vm);
        }
    }
}
