using TAKTORProject.ViewModels;
namespace TAKTORProject.Views;

public partial class ScorePage : ContentPage
{
	public ScorePage()
	{
		//init
		InitializeComponent();
		//set binding item from view model of this view
		BindingContext = new ScorePageViewModel();

	}
}