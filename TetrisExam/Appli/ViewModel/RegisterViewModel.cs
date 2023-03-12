using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Networking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TetrisExam.Appli.Model;
using TetrisExam.Appli.Service;
using TetrisExam.Appli.View;

namespace TetrisExam.Appli.ViewModel
{
    public partial class RegisterViewModel : BaseViewModel
    {
        private IUserService _userService;
        private IUserServiceSqlLite _userServiceSqlLite;

        private IConnectivity _connectivity;

        public RegisterViewModel(IUserService userService,IUserServiceSqlLite userServiceSqlLite, IConnectivity connectivity)
        {
            _userService = userService;
            _userServiceSqlLite= userServiceSqlLite;
            _connectivity = connectivity;
            
        }

        [ObservableProperty]
        private string _name;

        [ObservableProperty]
        private string _email;

        [ObservableProperty]
        private string _password;


        [RelayCommand]
        async void Register()
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(_email) && !string.IsNullOrWhiteSpace(_password) && !string.IsNullOrWhiteSpace(_name))
                {
                    Register register = new Register();
                    register.Email = _email;               
                    register.Name = _name;


                    

                    User user = new User();

                    if (_connectivity.NetworkAccess != NetworkAccess.Internet)
                    {
                        register.Password = BCrypt.Net.BCrypt.HashPassword(_password);
                        user = await _userServiceSqlLite.Register(register);
                    }
                    else
                    {
                        register.Password = _password;
                        user = await _userService.Register(register);
                    }


                    if (user != null)
                    {
                        await Shell.Current.GoToAsync(nameof(ProfilPage), true, new Dictionary<string, object>
                        {
                            {"User", user }
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Message", ex);
            }
        }


        [RelayCommand]
        async Task Back()
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}
