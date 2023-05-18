namespace TAKTORProject;

public partial class SummaryPage : ContentPage
{
	//Retrieve totalCost from CartPage
	public SummaryPage(string totalCost)
	{
		InitializeComponent();

		Total.Text = totalCost;
	}
    private async void Finish_Clicked(object sender, EventArgs e)
    {
		try
		{
			await DisplayAlert("", "ขอบคุณสำหรับการสั่งซื้อ", "ตกลง");
			await Shell.Current.GoToAsync("HOME");
		}catch (Exception ex)
		{
			await DisplayAlert("", ex.Message, "");
		}
    }
}