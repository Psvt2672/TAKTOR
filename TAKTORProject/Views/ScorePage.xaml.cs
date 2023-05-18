using TAKTORProject.ViewModels;
namespace TAKTORProject.Views;

public partial class ScorePage : ContentPage
{
	public ScorePage()
	{
		InitializeComponent();
		BindingContext = new ScorePageViewModel();

	}
}