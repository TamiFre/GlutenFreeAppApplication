using GlutenFreeApp.ViewModel;
using GlutenFreeApp.Views;
namespace GlutenFreeApp
{
    public partial class AppShell : Shell
    {
        public AppShell(AppShellViewModel viewModel)
        {
            this.BindingContext = viewModel;
            InitializeComponent();
            Routing.RegisterRoute("SignUp", typeof(SignUpView));
            Routing.RegisterRoute("Login", typeof(LoginPageView));
            Routing.RegisterRoute("AllRecipes", typeof(AllRecipeView));
            Routing.RegisterRoute("AddRecipe", typeof(AddRecipeView));
            Routing.RegisterRoute("AdminPage", typeof(AdminPageView));
        }
    }
}
