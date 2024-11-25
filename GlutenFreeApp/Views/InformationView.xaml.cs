using GlutenFreeApp.ViewModel;
namespace GlutenFreeApp.Views;

public partial class InformationView : ContentPage
{
	public InformationView(InformationViewModel vm)
	{
		this.BindingContext = vm;
		InitializeComponent();
	}
}