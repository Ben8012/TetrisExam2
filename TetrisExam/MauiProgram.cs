﻿using TetrisExam.Appli.Service;
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

        builder.Services.AddSingleton<BestScorePage>();
        builder.Services.AddSingleton<BestScoreViewModel>();

        

        builder.Services.AddScoped<UserService>();
		builder.Services.AddTransient<HttpClient>();


        return builder.Build();
	}
}
