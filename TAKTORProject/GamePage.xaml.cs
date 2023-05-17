namespace TAKTORProject;

public partial class GamePage : ContentPage
{
	public GamePage()
	{
		InitializeComponent();
	}
    private async void StartGameClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new GameMain());
    }

    private void HowToPlayClicked(object sender, EventArgs e)
    {

    }

    private void ScoreClicked(object sender, EventArgs e)
    {

    }
}