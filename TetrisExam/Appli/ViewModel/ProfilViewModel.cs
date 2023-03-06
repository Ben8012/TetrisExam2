
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TetrisExam.Appli.Model;
using TetrisExam.Appli.View;

namespace TetrisExam.Appli.ViewModel;

[QueryProperty(nameof(User), "User")]
public partial class ProfilViewModel : BaseViewModel
{
    [ObservableProperty]
    User user;

    [RelayCommand]
    async Task Game()
    {
        await Shell.Current.GoToAsync(nameof(GamePage), true, new Dictionary<string, object>
                    {
                        {"User", user }
                    });
    }

    [RelayCommand]
    async Task Back()
    {
        try
        {

            await Shell.Current.GoToAsync("..") ;
        }
        catch (Exception ex)
        {

            throw ex;
        }
        
    }
}
