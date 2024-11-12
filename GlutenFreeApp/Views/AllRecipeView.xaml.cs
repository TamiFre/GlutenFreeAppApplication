using GlutenFreeApp.ViewModel;

namespace GlutenFreeApp.Views;

public partial class AllRecipeView : ContentPage
{
	public AllRecipeView(AllRecipeViewModel vm)
	{
		this.BindingContext = vm;
		InitializeComponent();
	}
}