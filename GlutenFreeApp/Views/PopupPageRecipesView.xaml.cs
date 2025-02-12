using GlutenFreeApp.ViewModel;
namespace GlutenFreeApp.Views;

public partial class PopupPageRecipesView : ContentPage
{
	public PopupPageRecipesView(AllRecipeViewModel allRecipeViewModel)
	{
        this.BindingContext = allRecipeViewModel;
        InitializeComponent();
		
	}
}