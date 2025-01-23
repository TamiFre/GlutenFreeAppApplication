using GlutenFreeApp.ViewModel;
namespace GlutenFreeApp.Views;

public partial class AddRestaurantView : ContentPage
{
	public AddRestaurantView(AddRestaurantViewModel viewModel)
	{
		this.BindingContext = viewModel;
        InitializeComponent();
    }
}