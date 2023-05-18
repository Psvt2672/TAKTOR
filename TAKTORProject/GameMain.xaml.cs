using TAKTORProject.Models;
using TAKTORProject.ViewModels;
namespace TAKTORProject;

//หน้าหลักในการเล่นเกม
public partial class GameMain : ContentPage
{
    private int countClick = 0, countMove = 0, countCorrect = 0, queue = 0, totalMove = 0,totalScore;
    string score,a, timeText;
    string[,,] gamePath;
    private TimeOnly time = new();
    private bool timeRun = true;
    List<string> pathList = new List<string>();
    List<int> randRow, randCol,randomNumbers;
    private ImageButton btn1, btn2;
    

    public GameMain()
    {
        InitializeComponent();
        _ = A();
    }

    //กำหนดการทำงานของแต่ละ method ให้ method ก่อนหน้าทำงานเสร็จก่อนจุงค่อยทำ
    private async Task A()
    {
        Task a = ReadFile();
        await Task.WhenAll(a);
        Task b = CreateQueue();
        await Task.WhenAll(b);
        Task c = AddPuzzle();
        await Task.WhenAll(c);
        Timer();
    }

    //อ่านไฟล์ข้อมูล
    private async Task ReadFile()
    {

        string line;
        string targetFilePath = Path.Combine(FileSystem.AppDataDirectory, "gamepath_file.txt");
        using Stream fileStream = await FileSystem.Current.OpenAppPackageFileAsync("gamepath_file.txt");
        using (var reader = new StreamReader(fileStream))
        {
            while ((line = reader.ReadLine()) != null)
            {
                var values = line.Split(',');
                for (int i = 0; i < values.Length; i++)
                {
                    pathList.Add(values[i]);
                }
            }
        }
    }

    //สุ่มลำดับและภาพในเกม
    private async Task CreateQueue()
    {
        randRow = GetRandomNumbers(5);
        randCol = GetRandomNumbers(16);
        string[,] filePath = new string[5, 16];
        int index = 0;
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 16; j++)
            {
                filePath[i, j] = pathList[index];
                index++;
            }
        }
   
        gamePath = new string[5, 16, 2];
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 16; j++)
            {
                if (randCol[j] > ((16 / 2) - 1))
                {
                    randCol[j] %= 8;
                }
                gamePath[i, j, 0] = randCol[j].ToString();
                gamePath[i, j, 1] = filePath[i, j];
            }
        }
        string x="";
        for (int i = 0; i < 5; i++)
        {
            for (int j = 16 - 1; j >= 0; j--)
            {
                for (int k = 0; k < j; k++)
                {
                    if (gamePath[i, j, 0] == gamePath[i, k, 0])
                    {
                        a = gamePath[i, j, 1];
                        gamePath[i, j, 1] = gamePath[i, k, 1];
                        gamePath[i, k, 1] = a;
                        x += gamePath[i, k, 1];
                    }
                }
            }
        }
    }
    
    //แสดงปรืศนาบนจอ
    private async Task AddPuzzle()
    {
        int round = 0;
        countMove = 0;
        LCountMove.Text = "Move: " + countMove.ToString();
        LPuzzle.Text = "Puzzle " + (queue + 1).ToString();

        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                var btn = new ImageButton();
                btn.ClassId = gamePath[randRow[queue], round, 0];
                btn.Clicked += OnImageButtonClicked;
                btn.Source = gamePath[randRow[queue], round, 1];
                btn.HeightRequest = 100;
                btn.WidthRequest = 100;
                btn.HorizontalOptions = LayoutOptions.Center;
                Grid.SetRow(btn, i);
                Grid.SetColumn(btn, j);
                MainGrid.Children.Add(btn);
                round++;
            }
        }
    }

    //สุ่มเลข
    public List<int> GetRandomNumbers(int count)
    {
        randomNumbers = new List<int>();
        var random = new Random();
        int number;
        for (int i = 0; i < count; i++)
        {
            do
            {
                number = random.Next(0, count);
            }
            while (randomNumbers.Contains(number));
            randomNumbers.Add(number);
        }
        return randomNumbers;
    }

    //กดเลือกภาพ
    public async void OnImageButtonClicked(object sender, EventArgs e)
    {
        countClick++;
        if (countClick == 1)
        {
            btn1 = sender as ImageButton;
            btn1.IsEnabled = false;
        }
        else if (countClick == 2)
        {
            countMove++;
            LCountMove.Text = "Move: " + countMove.ToString();
            countClick = 0;
            btn2 = sender as ImageButton;
            if (btn1.ClassId == btn2.ClassId)
            {
                btn2.IsEnabled = false;
                SwapImage(btn1, btn2);
                countCorrect++;
            }
            else
            {
                btn1.IsEnabled = true;
                await DisplayAlert("Incorrect", "Try again ", "OK");
            }
        }
        if (countCorrect % 8 == 0 && countCorrect != 0)
        {
            MainGrid.Clear();
            countCorrect = 0;
            queue++;

            if (queue == randRow.Count())
            {
                timeText = LTimer.Text;
                timeRun = false;
                VLayout.Clear();
                score = CalScore(timeText);
                await DisplayAlert("Congreat", "You Finish All Puzzle", "ok");
                ShowScore();
            }
            else
            {
                totalMove += countMove;
                await AddPuzzle();
            }
        }
    }

    //แสดงผลคะแนน
    private async void ShowScore()
    {
        Label totalScore1 = new Label {Text = "Your Score",FontSize = 50, HorizontalOptions = LayoutOptions.Center, TextColor = Color.FromRgb(255, 255, 255) };
        VLayout.Add(totalScore1);
        Label totalScore2 = new Label {Text = score,FontSize = 80, HorizontalOptions = LayoutOptions.Center, TextColor = Color.FromRgb(255, 255, 255) };
        VLayout.Add(totalScore2);
        Button home = new Button { Text = "Back to game home" ,BackgroundColor= Color.FromRgb(255,80,180), TextColor = Color.FromRgb(255,255,255) };
        home.Clicked += HomeClicked;
        VLayout.Add(home);

        string username = Preferences.Get("Username", string.Empty);
        int sc = totalScore;
        string t = timeText.Remove(0, 6);

        ServiceRecord service = new ServiceRecord();
        await service.AddRec(username, sc, t);
    }

    //กดปุ่ม Back to game home
    private async void HomeClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

    //กดปุ่มquit game
    private async void QuitClicked(object sender, EventArgs e)
    {
        bool answer = await DisplayAlert("Quit Game", "Do you really want to exit?", "Yes", "No");
        if (answer)
        {
            timeRun = false;
            VLayout.Clear();
            await Navigation.PopAsync();
        }
        
    }

    //คำนวนคะแนน
    private string CalScore(string time)
    {
        time = time.Remove(0, 6);
        Double totalTime = Convert.ToDouble(time);
        Double cal = (1000 - ((totalMove - 40) * 5)) + (1000 - (totalTime * 100));
        totalScore = (int)cal;
        return totalScore.ToString();
    }

    //สลับภาพเมื่อกดเลือกถูก
    private void SwapImage(ImageButton swap1, ImageButton swap2)
    {
        var x = new ImageButton();
        x.Source = swap1.Source;
        swap1.Source = swap2.Source;
        swap2.Source = x.Source;

    }

    //นาฬิกาจับเวลา
    private async void Timer()
    {
        while (timeRun)
        {
            time = time.Add(TimeSpan.FromSeconds(1));
            LTimer.Text = "Time: " + $"{time.Minute}.{time.Second:00}";
            await Task.Delay(TimeSpan.FromSeconds(1));
        }
    }
}