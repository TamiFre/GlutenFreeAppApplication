using GlutenFreeApp.ViewModel;
namespace GlutenFreeApp.Views;

public partial class PopupPageView : ContentPage
{
	public PopupPageView(AllRestaurantsViewModel viewModel)
	{
        this.BindingContext = viewModel;
        InitializeComponent();


    }
}