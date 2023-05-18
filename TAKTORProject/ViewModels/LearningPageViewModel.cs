namespace TAKTORProject.ViewModels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TAKTORProject.Models;
using Newtonsoft.Json;

public partial class LearningPageViewModel : ObservableObject
{
	//tracking learning model : plain text for display
	[ObservableProperty]
	List<Learning> learnings;
	//tracking refreshing animation 
	[ObservableProperty]
	bool isRefreshing;

	public LearningPageViewModel()
	{
		readLearningPage();
	}
	//method for reading json file 
	[RelayCommand]
	public async void readLearningPage()
	{
		try
		{
			//wait for loading animation
			await Task.Delay(500);
			//define text file path
			var fileStream = await FileSystem.Current.OpenAppPackageFileAsync("textile.json");
			//read stream from that file 
			using (var reader = new StreamReader(fileStream))
			{
				//read and create object automatically
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
			//loading animation stop when finished load data
			IsRefreshing = false;
        }
    }
}
