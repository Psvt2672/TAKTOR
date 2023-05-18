namespace TAKTORProject.ViewModels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TAKTORProject.Models;
using Newtonsoft.Json;

public partial class LearningPageViewModel : ObservableObject
{

	[ObservableProperty]
	List<Learning> learnings;

	[ObservableProperty]
	bool isRefreshing;

	public LearningPageViewModel()
	{
		readLearningPage();
		
	}

	[RelayCommand]
	public async void readLearningPage()
	{
		try
		{
			await Task.Delay(2000);
			var fileStream = await FileSystem.Current.OpenAppPackageFileAsync("textile.json");
			using (var reader = new StreamReader(fileStream))
			{
				var contents = await reader.ReadToEndAsync();
				Learnings = JsonConvert.DeserializeObject<List<Learning>>(contents);
			}
		}
		catch(Exception ex)
		{
			Console.WriteLine(ex.Message);
        }
		finally
		{
			IsRefreshing = false;
        }
    }
}
