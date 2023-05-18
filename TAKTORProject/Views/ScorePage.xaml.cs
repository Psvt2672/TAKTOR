using TAKTORProject.ViewModels;
namespace TAKTORProject.Views;

public partial class ScorePage : ContentPage
{
	public ScorePage()
	{
		//init
		InitializeComponent();
		BindingContext = new ScorePageViewModel();

	}
}