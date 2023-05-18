using System.Security.Cryptography.X509Certificates;
using TAKTORProject.Views;

namespace TAKTORProject;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
        Routing.RegisterRoute("HOME", typeof(MainPage));
        Routing.RegisterRoute("STORE", typeof(StorePage));
        Routing.RegisterRoute("LEARNING", typeof(LearningPage));
        Routing.RegisterRoute("GAME", typeof(GamePage));
        Routing.RegisterRoute("SCORE", typeof(ScorePage));
    }
}
