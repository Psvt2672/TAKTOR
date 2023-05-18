using SQLite;
using TAKTORProject.Models;

namespace TAKTORProject;


public partial class SummaryPage : ContentPage
{
    public SQLiteAsyncConnection conn;
    //Retrieve totalCost from CartPage
    public SummaryPage(string totalCost)
	{
		InitializeComponent();
		//set control text 
		Total.Text = totalCost;
	}
    private async void Finish_Clicked(object sender, EventArgs e)
    {
		try
		{
            //delete item by user id
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Taktor.db3");
            conn = new SQLiteAsyncConnection(dbPath);
           
            int userId = Preferences.Get("Userid", 0);

            var orderTemp = await conn.Table<Order>().Where(x => x.UserId == userId).ToListAsync();
            foreach (var order in orderTemp)
            {
                await conn.DeleteAsync<Order>(order.OrderId);
            }

            await DisplayAlert("", "ขอบคุณสำหรับการสั่งซื้อ", "ตกลง");
            //Back to homepage
			await Shell.Current.GoToAsync("HOME");
		}catch (Exception ex)
		{
			await DisplayAlert("", ex.Message, "");
		}
    }
}