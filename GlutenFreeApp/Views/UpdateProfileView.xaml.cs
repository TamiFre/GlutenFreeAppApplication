using GlutenFreeApp.ViewModel;
namespace GlutenFreeApp.Views;

public partial class UpdateProfileView : ContentPage
{
	public UpdateProfileView(UpdateProfileViewModel viewModel)
	{
		this.BindingContext = viewModel;
        InitializeComponent();
    }
}