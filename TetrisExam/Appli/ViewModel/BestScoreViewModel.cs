using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TetrisExam.Appli.Model;
using TetrisExam.Appli.Service;
//using Microsoft.Maui.Essentials;

namespace TetrisExam.Appli.ViewModel
{
    public partial class BestScoreViewModel : BaseViewModel
    {
        public ObservableCollection<User> Users { get; } = new();

        [ObservableProperty]
        bool isRefreshing;

        private IUserService _userService;
        private IConnectivity _connectivity;
        private IUserServiceSqlLite _userServiceSqlLite;

        private List<User> users = new List<User>();

        public BestScoreViewModel (
            IUserService userService,
            IUserServiceSqlLite userServiceSqlLite,
            IConnectivity connectivity
            )
        {
            _userService = userService;
            _connectivity = connectivity;
            _userServiceSqlLite = userServiceSqlLite;
            GetBestScore();
            
        }


        //  recuperation des user si connection web dans l'api si pas dans sqlLite
        [RelayCommand]
        public async Task GetBestScore()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            users.Clear();
            if (_connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                try
                {
                    users = await _userServiceSqlLite.GetAllUsers();
                }
                catch (Exception ex)
                {
                    throw new Exception("Message", ex);
                }
            }
            else
            {
                try
                {
                    users = await _userService.GetAllUsers();
                }
                catch (Exception ex)
                {
                    throw new Exception("Message", ex);
                }
        
            }

            // trier par score , max au debut de liste
            users = users.OrderByDescending(x => x.Point).ToList();

            if (Users.Count != 0)
                Users.Clear();

            foreach (var user in users)
                Users.Add(user);


            IsBusy = false;
            IsRefreshing = false;

        }

        // retour vers la page precedante
        [RelayCommand]
        public async Task Back()
        {
            if (IsBusy)
                return;

            try
            {
                await Shell.Current.GoToAsync("..");
            }
            catch (Exception ex)
            {
                throw new Exception("Message", ex);
            }
            finally
            {
                IsBusy = false;
            }

        }

    }
}
