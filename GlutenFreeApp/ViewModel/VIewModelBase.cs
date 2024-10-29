namespace GlutenFreeApp.ViewModel;

public class VIewModelBase : ContentPage
{
	public VIewModelBase()
	{
		Content = new VerticalStackLayout
		{
			Children = {
				new Label { HorizontalOptions = LayoutOptions.Center, VerticalOptions = LayoutOptions.Center, Text = "Welcome to .NET MAUI!"
				}
			}
		};
	}
}