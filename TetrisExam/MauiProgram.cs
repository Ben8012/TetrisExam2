using TetrisExam.Appli.Service;
using TetrisExam.Appli.View;
using TetrisExam.Appli.ViewModel;

namespace TetrisExam;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddSingleton<MainPageViewModel>();

        builder.Services.AddSingleton<RegisterPage>();
        builder.Services.AddSingleton<RegisterViewModel>();

        builder.Services.AddSingleton<LoginPage>();
        builder.Services.AddSingleton<LoginViewModel>();

        builder.Services.AddSingleton<ProfilPage>();
        builder.Services.AddSingleton<ProfilViewModel>();

        builder.Services.AddSingleton<GamePage>();
        builder.Services.AddSingleton<GameViewModel>();

        builder.Services.AddTransient<BestScorePage>();
        builder.Services.AddTransient<BestScoreViewModel>();

        

        builder.Services.AddScoped< IUserService, UserService >();
		builder.Services.AddTransient<HttpClient>();

		
        string dbPath = FileAccessHelper.GetLocalFilePath("user.db3");
        builder.Services.AddSingleton< IUserServiceSqlLite, UserServiceSqlLite >(s => ActivatorUtilities.CreateInstance<UserServiceSqlLite>(s, dbPath));

        builder.Services.AddSingleton<IConnectivity>(Connectivity.Current);

        return builder.Build();
	}
}
