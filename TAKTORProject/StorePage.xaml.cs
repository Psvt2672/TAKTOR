using SQLite;
using TAKTORProject.Models;
using TAKTORProject.ViewModels;
namespace TAKTORProject;

public partial class StorePage : ContentPage
{
    public SQLiteAsyncConnection conn;
    public StorePage()
	{
		InitializeComponent();
        BindingContext = new ProductViewModel();
    }
    private async void Button_Clicked(object sender, EventArgs e)
    {
        string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Taktor.db3");
        conn = new SQLiteAsyncConnection(dbPath);
        conn.CreateTableAsync<Order>().Wait();

        Button btn = sender as Button;
        int productId = Int32.Parse(btn.ClassId);
        string xd = btn.ClassId;

        List<Product> prod = new List<Product>();
        prod = await conn.Table<Product>().Where(x => x.Id == productId).ToListAsync();

        Uri uriTemp = prod[0].Image;
        string productName = prod[0].Name;
        DateTime currentTime = DateTime.Now;
        int priceTemp = prod[0].Price;
        var newOrder = new Order()
        {
            ProductId = productId,
            UserId = Preferences.Get("Userid", 0),
            Date = currentTime,
            Image = uriTemp,
            ProductName = productName,
            Price = priceTemp
        };


        int rowsInserted = await conn.InsertAsync(newOrder);
        if (rowsInserted > 0)
        {

        }
        else
        {

        }
    }
    private async void Cart_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new CartPage());
    }
}