namespace GlutenFreeApp.Views;

public partial class InfoTabs : TabbedPage
{
	public InfoTabs(AllRecipeView tab1, InformationView tab2, AllRestaurantsView tab3)
	{
		InitializeComponent();
		NavigationPage p1 = new NavigationPage(tab1);
		p1.Title = "Kuku";
		p1.IconImageSource = "recipe.png";
		this.Children.Add(p1);
        this.Children.Add(tab2);
		this.Children.Add(tab3);

    }
}