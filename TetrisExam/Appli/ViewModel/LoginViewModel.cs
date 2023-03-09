﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using TetrisExam.Appli.Model;
using TetrisExam.Appli.Service;
using TetrisExam.Appli.View;

namespace TetrisExam.Appli.ViewModel
{
    public partial class LoginViewModel : BaseViewModel
    {
        private IUserService _userService;

        public LoginViewModel(IUserService userService)
        {
            _userService = userService;
        }

        [ObservableProperty]
        private string _email;

        [ObservableProperty]
        private string _password;


        [RelayCommand]
        async void Login()
        {
            try
            {
                //    if (!string.IsNullOrWhiteSpace(_email) && !string.IsNullOrWhiteSpace(_password))
                //    {
                Login login = new Login();
                login.Email = "tim@mail.com";
                login.Password = "test1234=";
                //login.Email = _email;
                //login.Password = _password;

                User user = await _userService.Login(login);

                if (user != null)
                {
                    await Shell.Current.GoToAsync(nameof(ProfilPage), true, new Dictionary<string, object>
                        {
                            {"User", user }
                        });
                }
                //}
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