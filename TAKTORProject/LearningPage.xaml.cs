namespace TAKTORProject;
using TAKTORProject.ViewModels;
public partial class LearningPage : ContentPage
{
	public LearningPage()
	{
		InitializeComponent();

		BindingContext = new LearningPageViewModel();
	}
}