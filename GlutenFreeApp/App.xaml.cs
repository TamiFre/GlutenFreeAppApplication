using GlutenFreeApp.Views;
using GlutenFreeApp.ViewModel;
using GlutenFreeApp.Models;
using GlutenFreeApp.Services;

namespace GlutenFreeApp
{
    public partial class App : Application
    {

        public UsersInfo? LoggedInUser { get; set; }
        public List<StatusInfo> Statuses = new List<StatusInfo>();
        public List<TypeFoodInfo> FoodTypes = new List<TypeFoodInfo>();
        private GlutenFreeServiceWebAPIProxy proxy;

        public App(IServiceProvider serviceProvider, GlutenFreeServiceWebAPIProxy proxy)
        {
            this.proxy = proxy;
            InitializeComponent();
            LoginPageView? v = serviceProvider.GetService<LoginPageView>();
            //SignUpView? v = serviceProvider.GetService<SignUpView>();
            LoadBasicData();
            MainPage = new NavigationPage(v);
        }

        public async void LoadBasicData()
        {
            //this gets the data of the statuses and food types
            Statuses = await this.proxy.GetAllStatuses();           
            FoodTypes = await this.proxy.GetAllFoodTypes();
        }
    }
}
