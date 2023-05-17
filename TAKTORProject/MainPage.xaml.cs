namespace TAKTORProject;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
        // ดึงข้อมูลจาก SignIn
        string username = Preferences.Get("Username", string.Empty);
        // เก็บการ Binding
        BindingContext = new MainPageViewModel { Username = username };
    }
    public class MainPageViewModel
    {
        public string Username { get; set; }
    }
    private async void Store_clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("STORE");
    }
    private async void Learning_clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("LEARNING");
    }
    private async void Game_clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("GAME");
    }
    private async void OnLogoutButtonClicked(object sender, EventArgs e)
    {
        Preferences.Remove("Username");
        Preferences.Clear();
        await Navigation.PopToRootAsync();
    }
}

