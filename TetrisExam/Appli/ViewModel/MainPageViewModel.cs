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
                // _userServiceSqlLite.Delete();
                UpdateSqlLite();
            }
        }

        private async Task UpdateSqlLite()
        {
           
            usersDB = await _userService.GetAllUsers();
            usersSqlLite = await _userServiceSqlLite.GetAllUsers();

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

            
            



        usersDB = await _userService.GetAllUsers();
            await _userServiceSqlLite.Delete();

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
