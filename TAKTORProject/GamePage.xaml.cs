using TAKTORProject.Views;

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

    private async void HowToPlayClicked(object sender, EventArgs e)
    {
        await DisplayAlert("How to play", "เมื่อเริ่มเกมจะปรากฎปริศนาภาพลายผ้าทอพื้นเมืองมาให้ ผู้เล่นจะต้องทำการกดเลือกชื้นส่วนของภาพ 2 ภาพเพื่อสลับให้ลายของผ้าทอพื้นเมืองไปอยู่ในช่องถูกต้องตามรูปแบบ หากกดเลือกภาพที่ต้องการสลับผิดจะมีข้อความเตือนขึ้นมา จำนวนการกดสลับภาพก้เพิ่มขึ้นด้วย โดยผู้เล่นจะต้องทำการเล่นทั้งหมด 5 ด่าน โดยจะสุ่มภาพและตำแหน่งปริศนาให้ใหม่ทุกครั้งที่เริ่มเกม ผู้เล่นที่กดสลับตำแหน่งและใช้เวลาในการเล่นน้อยจะได้คะแนนสูง", "OK!");
    }

    private async void ScoreClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ScorePage());
    }
}