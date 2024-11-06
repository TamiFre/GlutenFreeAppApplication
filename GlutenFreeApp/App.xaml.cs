using GlutenFreeApp.Views;
using GlutenFreeApp.ViewModel;

namespace GlutenFreeApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }
    }
}
