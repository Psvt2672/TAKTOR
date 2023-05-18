using SQLite;
using TAKTORProject.ViewModels;
using TAKTORProject.Models;
namespace TAKTORProject;

public partial class CartPage : ContentPage
{
    public SQLiteAsyncConnection conn;
    public SQLiteAsyncConnection delconn;
    private Label test = new Label();
    public CartPage()
	{
		InitializeComponent();
        BindingContext = new OrderViewModel();
        VLayout.Add(test);
    }
    public async void Summary_Clicked (object sender, EventArgs e)
    {
        string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Taktor.db3");
        conn = new SQLiteAsyncConnection(dbPath);
        conn.CreateTableAsync<Order>().Wait();

        int userIdTemp = Preferences.Get("Userid", 0);
        List<Order> ord = new List<Order>();
        ord = await conn.Table<Order>().Where(x => x.UserId == userIdTemp).ToListAsync();

        List<int> priceSum = new List<int>();
        foreach (Order order in ord)
        {
            priceSum.Add(order.Price);
        }
        int summary = priceSum.Sum();

        //test.Text = summary.ToString();
        string TotalCost = summary.ToString();
        await Navigation.PushAsync(new SummaryPage(TotalCost));
    }
    private async void Delete_Clicked(object sender, EventArgs e)
    {
        Button btn = sender as Button;
        int productId = Int32.Parse(btn.ClassId);
        string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Taktor.db3");
        delconn = new SQLiteAsyncConnection(dbPath);
        delconn.DeleteAsync<Order>(productId);

        await Navigation.PushAsync(new CartPage());
    }
}