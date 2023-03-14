using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TetrisExam.Appli.Model;
using TetrisExam.Appli.Model.SqlLite;
using TetrisExam.Appli.Service;
using TetrisExam.Appli.View;

namespace TetrisExam.Appli.ViewModel
{
    public partial class MainPageViewModel : BaseViewModel
    {
        private IUserService _userService;
        private IUserServiceSqlLite _userServiceSqlLite;
        private IConnectivity _connectivity;

        private List<Register> registers = new List<Register>();
        private List<User> usersDB = new List<User>();
        private List<User> usersSqlLite = new List<User>();

        public MainPageViewModel(IUserService userService, IUserServiceSqlLite userServiceSqlLite, IConnectivity connectivity)
        {
            Title = "Tetris";
            _userService= userService;
            _userServiceSqlLite= userServiceSqlLite;
            _connectivity= connectivity;

            // ici mise a jour sql lite
            if (_connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                // me premet du supprimer les user dans SqlLite pour les test
                // _userServiceSqlLite.Delete(); 

                // mis a jour de sqlLite par rapport au data user en DB
                UpdateSqlLite();
            }
        }

        private async Task UpdateSqlLite()
        {
            // recuperation de tout les user en DB
            usersDB = await _userService.GetAllUsers();
            // recuperation de tout les user en Sqllite
            usersSqlLite = await _userServiceSqlLite.GetAllUsers();

            // comparaison des 2 listes + insertion des user dont l'email et different en DB
            bool existe = false;
            foreach (var userSqlLite  in usersSqlLite )
            {
                existe = false;
                foreach (var userDB in usersDB)
                {
                    if (userDB.Email == userSqlLite.Email)
                    {
                       existe= true;
                    }
                }
                if (!existe)
                {
                    Register register = new Register()
                    {
                        Name = userSqlLite.Name,
                        Email = userSqlLite.Email,
                        Point = userSqlLite.Point,
                        IsActive = userSqlLite.IsActive,
                        Password = "test1234="

                    };
                    User result = await _userService.Register(register);
                }
                existe= false;
            }

            
            // recuperation des user en DB mis a jour 
            usersDB = await _userService.GetAllUsers();

            // suppression des user dans SqlLite
            await _userServiceSqlLite.Delete();


            // insertion des users de la db dans Sqllite
            foreach (var userDB in usersDB)
            {
                Register register = new Register()
                {

                    Name = userDB.Name,
                    Email = userDB.Email,
                    Point = userDB.Point,
                    IsActive = userDB.IsActive
                };
                User user = await _userServiceSqlLite.Register(register);
            }

        }


        // redirection vers Register
        [RelayCommand]
         async Task GoToRegister()
        {
            if (IsBusy) return;

            try
            {
                await Shell.Current.GoToAsync(nameof(RegisterPage));
            }
            catch (Exception ex)
            {
                throw new Exception("Message", ex);
            }
        }


        // redirection vers Login
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
                throw new Exception("Message", ex);
            }
        }


        // redirection vers les meilleurs scores
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
                throw new Exception("Message", ex);
            }
        }
    }
}
