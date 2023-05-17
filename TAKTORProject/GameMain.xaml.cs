namespace TAKTORProject;

public partial class GameMain : ContentPage
{
    private int countClick = 0, countMove = 0, countCorrect = 0, queue = 0, totalMove = 0;
    private ImageButton btn1, btn2;
    string score;
    string[,,] gamePath;
    List<int> randRow, randCol;
    private TimeOnly time = new();
    private bool timeRun = true;

    public GameMain()
    {
        InitializeComponent();
        CreateQueue();
        AddPuzzle();
        Timer();

    }
    private async void Timer()
    {
        while (timeRun)
        {
            time = time.Add(TimeSpan.FromSeconds(1));
            LTimer.Text = "Time: " + $"{time.Minute}.{time.Second:00}";
            await Task.Delay(TimeSpan.FromSeconds(1));
        }
    }

    private async void CreateQueue()
    {
        List<string> pathList = new List<string>();
        int row = 0, col = 0;
        string a;
        string line;

        //ควรจะใช้อันนี้แต่มีปัญหาง่ะ
        try
        {
            using Stream fileStream = await FileSystem.Current.OpenAppPackageFileAsync("gamepath_file.txt");
            using StreamReader reader = new StreamReader(fileStream);
            while ((line = reader.ReadLine()) != null)
            {
                var values = line.Split(',');
                col = values.Length;
                for (int i = 0; i < values.Length; i++)
                {
                    pathList.Add(values[i]);
                }
                row++;
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }

        //ใช้ไปก่อน/////////////////////////////////////////
       /*
        using (var reader = new StreamReader("D:\\gamepath_file.txt"))
        {
            while (!reader.EndOfStream)
            {
                line = reader.ReadLine();
                var values = line.Split(',');
                col = values.Length;
                for (int i = 0; i < values.Length; i++)
                {
                    pathList.Add(values[i]);
                }
                row++;
            }
        }
        */

        string[,] filePath = new string[row, col];
        int index = 0;
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < col; j++)
            {
                filePath[i, j] = pathList[index];
                index++;
            }
        }

        randRow = GetRandomNumbers(row);
        randCol = GetRandomNumbers(col);
        gamePath = new string[row, col, 2];

        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < col; j++)
            {
                if (randCol[j] > ((col / 2) - 1))
                {
                    randCol[j] %= (col / 2);
                }
                gamePath[i, j, 0] = randCol[j].ToString();
                gamePath[i, j, 1] = filePath[i, j];
            }

        }

        for (int i = 0; i < row; i++)
        {
            for (int j = col - 1; j >= 0; j--)
            {
                for (int k = 0; k < j; k++)
                {
                    if (gamePath[i, j, 0] == gamePath[i, k, 0])
                    {
                        a = gamePath[i, j, 1];
                        gamePath[i, j, 1] = gamePath[i, k, 1];
                        gamePath[i, k, 1] = a;
                    }

                }
            }
        }

    }
    private void AddPuzzle()
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



    public static List<int> GetRandomNumbers(int count)
    {
        List<int> randomNumbers = new List<int>();
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
            LCountMove.Text = "move: " + countMove.ToString();
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
                string t = LTimer.Text;
                timeRun = false;
                LabelGrid.Clear();
                score = Score(t);
                await DisplayAlert("Congreat", "You Finish All Puzzle", "ok");
                ShowScore();
            }
            else
            {
                totalMove += countMove;
                AddPuzzle();

            }
        }
    }

    private void ShowScore()
    {
        Label totalScore1 = new Label();
        totalScore1.Text = "Your Score";
        totalScore1.FontSize = 50;
        VLayout.Add(totalScore1);

        Label totalScore2 = new Label();
        totalScore2.Text = score;
        totalScore2.FontSize = 80;
        VLayout.Add(totalScore2);


    }
    private string Score(string time)
    {
        time = time.Remove(0, 6);
        Double totalTime = Convert.ToDouble(time);
        Double cal = (1000 - (totalMove - (8 * randRow.Count())) + (10000 - (totalTime * 100)));
        int score = (int)cal;
        return score.ToString();
    }

    private void SwapImage(ImageButton swap1, ImageButton swap2)
    {
        var x = new ImageButton();
        x.Source = swap1.Source;
        swap1.Source = swap2.Source;
        swap2.Source = x.Source;

    }
}