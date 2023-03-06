using TetrisExam.Appli.ViewModel;

namespace TetrisExam.Appli.View;

public partial class BestScorePage : ContentPage
{
	public BestScorePage(BestScoreViewModel bestScoreViewModel)
	{
		InitializeComponent();
		BindingContext= bestScoreViewModel;
	}
}