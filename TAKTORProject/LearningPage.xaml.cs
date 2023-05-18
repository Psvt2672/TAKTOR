namespace TAKTORProject;
using TAKTORProject.ViewModels;
public partial class LearningPage : ContentPage
{
	public LearningPage()
	{
		InitializeComponent();
		//set binding context for databinding from view model
		BindingContext = new LearningPageViewModel();
	}
}