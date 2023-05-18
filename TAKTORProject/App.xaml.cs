
using Microsoft.Maui.Controls;
namespace TAKTORProject;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		MainPage = new AppShell();
	}
}
