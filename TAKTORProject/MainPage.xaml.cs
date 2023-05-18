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
    private async void Home_Clicked(object sender, EventArgs e)
    {
        ImageButton btn = sender as ImageButton;
        string path = btn.ClassId;
        await Shell.Current.GoToAsync(path);
    }
   
    private async void OnLogoutButtonClicked(object sender, EventArgs e)
    {
        Preferences.Remove("Username");
        Preferences.Clear();
        await Navigation.PopToRootAsync();
    }
}

