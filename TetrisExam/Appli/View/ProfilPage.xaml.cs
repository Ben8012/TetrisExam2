using TetrisExam.Appli.ViewModel;

namespace TetrisExam.Appli.View;

public partial class ProfilPage : ContentPage
{
	public ProfilPage(ProfilViewModel profilViewModel)
	{
		InitializeComponent();
		BindingContext= profilViewModel;
	}
}