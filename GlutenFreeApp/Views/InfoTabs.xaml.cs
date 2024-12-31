namespace GlutenFreeApp.Views;

public partial class InfoTabs : TabbedPage
{
	public InfoTabs(AllRecipeView tab1, InformationView tab2, AllRestaurantsView tab3)
	{
		InitializeComponent();
		this.Children.Add(tab1);
        this.Children.Add(tab2);
		this.Children.Add(tab3);

    }
}