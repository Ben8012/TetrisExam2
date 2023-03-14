
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
    private IUserServiceSqlLite _userServiceSqlLite;
    private IConnectivity _connectivity;
    public ProfilViewModel(IUserService userService, IUserServiceSqlLite userServiceSqlLite, IConnectivity connectivity)
    {
        _userService = userService;
        _userServiceSqlLite = userServiceSqlLite;
        _connectivity = connectivity;
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
                update.Id = user.Id;
                update.Name = User.Name;

                if (_connectivity.NetworkAccess == NetworkAccess.Internet)
                {
                    user = await _userService.Update(update);
                }
                else
                {
                    user = await _userServiceSqlLite.Update(update);
                }

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
            if (_connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                await _userService.Delete(User.Id);
            }
            else
            {
                await _userServiceSqlLite.Delete(User);
            }

           
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
