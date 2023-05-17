using SQLite;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Text;

namespace TAKTORProject;

public partial class RegistrationPage : ContentPage
{
    private SQLiteAsyncConnection _connection;
    public RegistrationPage()
    {
        InitializeComponent();
        string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "users.db3");
        _connection = new SQLiteAsyncConnection(dbPath);
        _connection.CreateTableAsync<User>().Wait();
    }
    public class User
    {
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
            Password = RegistrationPage.HashPassword(password);
        }
    }
    // ฟังค์ชั่นสร้างแฮชของรหัสผ่าน
    public static string HashPassword(string password)
    {
        using (var md5 = MD5.Create())
        {
            byte[] inputBytes = Encoding.ASCII.GetBytes(password);
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                builder.Append(hashBytes[i].ToString("x2"));
            }
            return builder.ToString();
        }
    }

    private async void OnSignInButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PopToRootAsync();
        //await Navigation.PushAsync(new SignInPage());
    }
    private async void OnCreateAccountButtonClicked(object sender, EventArgs e)
    {

        // เช็คว่ามีช่องที่ไม่ใส่ไหม
        if (string.IsNullOrWhiteSpace(EmailEntry.Text) || string.IsNullOrWhiteSpace(UsernameEntry.Text) ||
            string.IsNullOrWhiteSpace(PasswordEntry.Text) || string.IsNullOrWhiteSpace(ConfirmPasswordEntry.Text))
        {
            await DisplayAlert("ผิดพลาด", "กรุณากรอกข้อมูลให้ครบทุกช่อง", "ตกลง");
            return;
        }

        // เช็ครหัสผ่าน
        if (HashPassword(PasswordEntry.Text) != HashPassword(ConfirmPasswordEntry.Text))
        {
            await DisplayAlert("ผิดพลาด", "รหัสผ่านทั้ง 2 อันไม่ตรงกัน", "ตกลง");
            return;
        }

        //รหัสผ่านต้องเกิน 6 ตัว
        if (PasswordEntry.Text.Length < 6)
        {
            await DisplayAlert("ผิดพลาด", "รหัสผ่านต้องมีความยาว 6 ตัวขึ้นไป", "ตกลง");
            return;
        }

        // เช็ค format email
        if (!Regex.IsMatch(EmailEntry.Text, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
        {
            await DisplayAlert("ผิดพลาด", "รูปแบบของอีเมลไม่ถูกต้อง", "ตกลง");
            return;
        }

        // เช็คเมลซ้ำ
        var existingUser = await _connection.Table<User>().Where(u => u.Email == EmailEntry.Text).FirstOrDefaultAsync();
        if (existingUser != null)
        {
            await DisplayAlert("ผิดพลาด", "อีเมลนี่ได้ถูกใช้งานแล้ว", "ตกลง");
            return;
        }
        // ใส่ข้อมูล
        var newUser = new User(EmailEntry.Text, UsernameEntry.Text, HashPassword(PasswordEntry.Text))
        {
            Email = EmailEntry.Text,
            Username = UsernameEntry.Text,
            Password = HashPassword(PasswordEntry.Text)
        };

        // ถ้าสร้างได้
        int rowsInserted = await _connection.InsertAsync(newUser);
        if (rowsInserted > 0)
        {
            await DisplayAlert("สำเร็จ", "สร้างผู้ใช้งานสำเร็จ", "ตกลง");
            //await Navigation.PushAsync(new SignInPage());
            await Navigation.PopToRootAsync();
        }
        else
        {
            await DisplayAlert("ผิดพลาด", "สร้างผู้ใช้งานไม่สำเร็จ", "ตกลง");
        }
    }
}