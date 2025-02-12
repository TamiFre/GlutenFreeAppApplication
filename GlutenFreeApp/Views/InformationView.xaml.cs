using GlutenFreeApp.ViewModel;
namespace GlutenFreeApp.Views;

public partial class InformationView : ContentPage
{
	public InformationView(InformationViewModel vm)
	{
		this.BindingContext = vm;
		InitializeComponent();
	}
    private async void OnFactChanged()
    {
        // Reset the scale and opacity before starting the animation
        FactLabel.Scale = 0.1;
        FactLabel.Opacity = 0;

        // Create a new animation for scaling and fading
        var scaleUp = FactLabel.ScaleTo(1, 500); // Scale to 1 over 500ms
        var fadeIn = FactLabel.FadeTo(1, 500);  // Fade to full opacity over 500ms

        // Wait for both animations to finish
        await Task.WhenAll(scaleUp, fadeIn);
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        // Subscribe to the PropertyChanged event to track when CurrentFact changes
        var viewModel = (InformationViewModel)BindingContext;
        viewModel.PropertyChanged += (sender, args) =>
        {
            if (args.PropertyName == nameof(InformationViewModel.CurrentFact))
            {
                // Call the animation method when the fact changes
                OnFactChanged();
            }
        };
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        var viewModel = (InformationViewModel)BindingContext;
        viewModel.PropertyChanged -= (sender, args) =>
        {
            if (args.PropertyName == nameof(InformationViewModel.CurrentFact))
            {
                OnFactChanged();
            }
        };
    }
}