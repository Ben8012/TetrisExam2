using TetrisExam.Appli.ViewModel;

namespace TetrisExam.Appli.View;

public partial class RegisterPage : ContentPage
{
	public RegisterPage(RegisterViewModel registerViewModel)
	{
		InitializeComponent();
		BindingContext= registerViewModel;
	}
}