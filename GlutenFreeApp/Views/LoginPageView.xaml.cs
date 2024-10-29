using GlutenFreeApp.ViewModel;

namespace GlutenFreeApp.Views;

public partial class LoginPageView : ContentPage
{
	public LoginPageView(LoginPageViewModel vm)
	{
		this.BindingContext = vm;
		InitializeComponent();
	}
}