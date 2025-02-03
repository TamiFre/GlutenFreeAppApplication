using GlutenFreeApp.ViewModel;
namespace GlutenFreeApp.Views;

public partial class PopupPageRecipesView : ContentPage
{
	public PopupPageRecipesView(AdminPageViewModel adminPageViewModel)
	{
        this.BindingContext = adminPageViewModel;
        InitializeComponent();
		
	}
}