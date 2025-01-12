using GlutenFreeApp.ViewModel;
namespace GlutenFreeApp.Views;

public partial class UpdateRestaurantProfileView : ContentPage
{
	public UpdateRestaurantProfileView(UpdateRestaurantProfileViewModel viewModel)
	{
		this.BindingContext = viewModel;
        InitializeComponent();
    }
}