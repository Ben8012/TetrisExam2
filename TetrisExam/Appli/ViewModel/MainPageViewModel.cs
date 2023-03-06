using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TetrisExam.Appli.View;

namespace TetrisExam.Appli.ViewModel
{
    public partial class MainPageViewModel : BaseViewModel
    {


        public MainPageViewModel()
        {
            Title = "Tetris";
        }

        [RelayCommand]
         async Task GoToRegister()
        {
            if (IsBusy) return;

            try
            {
                await Shell.Current.GoToAsync(nameof(RegisterPage));
            }
            catch (Exception)
            {

                throw;
            }
        }

        [RelayCommand]
         async Task GoToLogin()
        {
            if (IsBusy) return;

            try
            {
                await Shell.Current.GoToAsync(nameof(LoginPage));
            }
            catch (Exception ex )
            {

                throw;
            }
        }

        [RelayCommand]
        async Task GoToBestScore()
        {
            if (IsBusy) return;

            try
            {
                await Shell.Current.GoToAsync(nameof(BestScorePage));
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
