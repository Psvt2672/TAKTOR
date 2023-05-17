using System.Security.Cryptography.X509Certificates;

namespace TAKTORProject;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
        Routing.RegisterRoute("STORE", typeof(StorePage));
        Routing.RegisterRoute("LEARNING", typeof(LearningPage));
        Routing.RegisterRoute("GAME", typeof(GamePage));
    }
}
