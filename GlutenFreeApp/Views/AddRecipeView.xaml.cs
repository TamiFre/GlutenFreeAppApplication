using GlutenFreeApp.ViewModel;
namespace GlutenFreeApp.Views;


public partial class AddRecipeView : ContentPage
{
	public AddRecipeView( AddRecipeViewModel vm)
	{
		this.BindingContext = vm;
		InitializeComponent();
	}
}