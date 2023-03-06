using TetrisExam.Appli.ViewModel;

namespace TetrisExam.Appli.View;

public partial class GamePage : ContentPage
{
	public GamePage(GameViewModel gameViewModel)
	{
		InitializeComponent();
		BindingContext= gameViewModel;
	}
}