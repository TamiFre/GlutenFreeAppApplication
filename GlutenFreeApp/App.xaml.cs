using GlutenFreeApp.Views;
using GlutenFreeApp.ViewModel;

namespace GlutenFreeApp
{
    public partial class App : Application
    {
        public App(SignUpViewModel vm)
        {
            InitializeComponent();

            MainPage = new SignUpView(vm);
        }
    }
}
