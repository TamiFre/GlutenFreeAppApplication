namespace GlutenFreeApp.Views;

public partial class InfoTabs : TabbedPage
{
	public InfoTabs(AllRecipeView tab1, InformationView tab2, AllRestaurantsView tab3)
	{
		InitializeComponent();
		NavigationPage p1 = new NavigationPage(tab1);
		p1.Title = "All Recipes";
		p1.IconImageSource = "recipe.png";
		NavigationPage p2 = new NavigationPage(tab2);
		p2.Title = "Fun Facts";
		p2.IconImageSource = "fun.png";
		NavigationPage p3 = new NavigationPage(tab3);
		p3.Title = "All Restaurants";
		p3.IconImageSource = "restaurant.png";
        this.Children.Add(p1);
        this.Children.Add(p3);
		this.Children.Add(p2);

    }
}