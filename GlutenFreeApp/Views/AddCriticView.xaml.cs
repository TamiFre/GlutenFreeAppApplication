using GlutenFreeApp.ViewModel;

namespace GlutenFreeApp.Views;

public partial class AddCriticView : ContentPage
{
	public AddCriticView(AddCriticViewModel vm)
	{
		this.BindingContext = vm;
		InitializeComponent();
	}
}