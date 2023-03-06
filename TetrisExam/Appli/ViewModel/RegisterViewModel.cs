using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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
        private UserService _userService;

        public RegisterViewModel(UserService userService)
        {
            _userService = userService;

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
                    register.Password = _password;
                    register.Name = _name;

                    User user = await _userService.Register(register);

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
                throw ex;
            }
        }


        [RelayCommand]
        async Task Back()
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}
