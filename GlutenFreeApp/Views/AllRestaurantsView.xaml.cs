using GlutenFreeApp.ViewModel;
namespace GlutenFreeApp.Views;

public partial class AllRestaurantsView : ContentPage
{
	public AllRestaurantsView(AllRestaurantsViewModel viewModel)
	{
		this.BindingContext = viewModel;
		InitializeComponent();
	}
}