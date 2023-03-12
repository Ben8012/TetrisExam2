
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TetrisExam.Appli.Model;
using TetrisExam.Appli.Service;
using TetrisExam.Appli.View;

namespace TetrisExam.Appli.ViewModel;

[QueryProperty(nameof(User), "User")]
public partial class ProfilViewModel : BaseViewModel
{
    [ObservableProperty]
    User user;

   

    private IUserService _userService;
    public ProfilViewModel(IUserService userService)
    {
        _userService= userService;
    }

    [RelayCommand]
    async Task Game()
    {
        await Shell.Current.GoToAsync(nameof(GamePage), true, new Dictionary<string, object>
                    {
                        {"User", User }
                    });
    }


    [RelayCommand]
    async void Update()
    {
        try
        {
            if (!string.IsNullOrWhiteSpace(User.Email)  && !string.IsNullOrWhiteSpace(User.Name))
            {
                Update update = new Update();
                update.Email = User.Email;
                update.Id = User.Id;
                update.Name = User.Name;

                User user = new User();

                user = await _userService.Update(update);
                
            }
        }
        catch (Exception ex)
        {
            throw new Exception("Message", ex);
        }
    }


    [RelayCommand]
    async Task Delete()
    {
        try
        {
            await _userService.Delete(User.Id);
            await Shell.Current.GoToAsync("..");
        }
        catch (Exception ex)
        {
            throw new Exception("Message", ex);
        }
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
            throw new Exception("Message", ex);
        }
        
    }
}
