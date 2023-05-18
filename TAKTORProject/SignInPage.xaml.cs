using SQLite;

namespace TAKTORProject;

public partial class SignInPage : ContentPage
{
    private SQLiteAsyncConnection _connection;
    public SignInPage()
    {
        InitializeComponent();
        //defind file path of Database file
        string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Taktor.db3");
        //create connection
        _connection = new SQLiteAsyncConnection(dbPath);
    }

    //Model User
    public class User
    {
        //Attribute in DB
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public User()
        { }

        public User(string email, string username, string password)
        {
            Email = email;
            Username = username;
            Password = password;
        }
    }
    private void OnSignUpButtonClicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new RegistrationPage());
    }
    private async void OnSignInButtonClicked(object sender, EventArgs e)
    {
        //เช็คว่าใส่ชื่อกับรหัสผ่านรึยัง?
        string email = emailEntry.Text;
        string password = passwordEntry.Text;

        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
        {
            await DisplayAlert("ผิดพลาด", "กรุณากรอกช่องอีเมลและรหัสผ่านให้ถูกต้อง", "ตกลง");
            return;
        }

        //การยืนยันตัว
        var hashedPassword = RegistrationPage.HashPassword(password);
        var user = await _connection.Table<User>()
            .Where(u => u.Email == emailEntry.Text && u.Password == hashedPassword).FirstOrDefaultAsync();
        if (user == null)
        {
            await DisplayAlert("ผิดพลาด", "อีเมลหรือรหัสผ่านไม่ถูกต้อง", "OK");
            return;
        }
        await DisplayAlert("สำเร็จ", "ยินดีต้อนรับคุณ, " + user.Username + "!", "ตกลง");

        //เก็บข้อมูลผู้ใช้งาน หลังจาก Login เข้าสู่ระบบ
        Preferences.Set("Username", user.Username);
        Preferences.Set("Userid", user.Id);
        await Navigation.PushAsync(new MainPage()); // หลังเข้าได้อยากให้ไปหน้าไหนให้แก้ตรงนี้!!
    }
}