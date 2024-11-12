using GlutenFreeApp.ViewModel;

namespace GlutenFreeApp.Views;

public partial class AdminPageView : ContentPage
{
	public AdminPageView(AdminPageViewModel vm)
	{
		this.BindingContext = vm;
		InitializeComponent();
	}
}