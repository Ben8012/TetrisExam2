using TetrisExam.Appli.View;

namespace TetrisExam;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

        //Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
        Routing.RegisterRoute(nameof(RegisterPage), typeof(RegisterPage));
        Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
        Routing.RegisterRoute(nameof(ProfilPage), typeof(ProfilPage));
        Routing.RegisterRoute(nameof(GamePage), typeof(GamePage)); 
        Routing.RegisterRoute(nameof(BestScorePage), typeof(BestScorePage));
    }
}
