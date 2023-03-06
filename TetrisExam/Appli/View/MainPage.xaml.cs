using TetrisExam.Appli.ViewModel;

namespace TetrisExam.Appli.View;

public partial class MainPage : ContentPage
{
	public MainPage(MainPageViewModel mainPageViewModel)
	{
        InitializeComponent();
        BindingContext = mainPageViewModel;
    }
	
}

