﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private IUserServiceSqlLite _userServiceSqlLite;
        private IConnectivity _connectivity;

        public LoginViewModel(
            IUserService userService,
            IUserServiceSqlLite userServiceSqlLite,
            IConnectivity connectivity
            )
        {
            _userService = userService;
            _userServiceSqlLite = userServiceSqlLite;
            _connectivity = connectivity;
        }

        [ObservableProperty]
        private string _email;

        [ObservableProperty]
        private string _password;


        
        [RelayCommand]
        public async Task Login()
        {
            try
            {
                Login login = new Login();
                // Pour faire un connection sans devoir encoder l email et le mot de passe
                //login.Email = "tim@mail.com";
                //login.Password = "test1234=";


                login.Email = _email;
                login.Password = _password;


                User user = new User();
                // si connection web login vers api si pas login vers SqlLite
                if (_connectivity.NetworkAccess == NetworkAccess.Internet)
                {
                    user = await _userService.Login(login);
                }
                else
                {
                    user = await _userServiceSqlLite.Login(login);
                }

                

                // redirection apres le login , (mettre la redirection en commentaire pour le test du login!)
                if (user != null)
                {
                    await Shell.Current.GoToAsync(nameof(ProfilPage), true, new Dictionary<string, object>
                            {
                                {"User", user }
                            });
                }
               
            }
            catch (Exception ex)
            {
                throw new Exception("Message", ex);
            }
        }

        // retour vers la page precedante
        [RelayCommand]
        async Task Back()
        {
            await Shell.Current.GoToAsync("..");
        }

    }
}
