using TetrisExam.Appli.ViewModel;

namespace TetrisExam.Appli.View;

public partial class LoginPage : ContentPage
{
	public LoginPage(LoginViewModel loginViewModel)
	{
		InitializeComponent();
		BindingContext= loginViewModel;
	}
}