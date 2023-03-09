using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TetrisExam.Appli.Model;
using TetrisExam.Appli.Service;

namespace TetrisExam.Appli.ViewModel
{
    public partial class BestScoreViewModel : BaseViewModel
    {
        public ObservableCollection<User> Users { get; } = new();

        [ObservableProperty]
        bool isRefreshing;

        private IUserService _userService;
      
        public BestScoreViewModel (IUserService userService)
        {
            _userService = userService;
            //GetBestScore();
        }

        [RelayCommand]
        public async Task GetBestScore()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;
            

                List<User>users = await _userService.GetAllUsers();
                users = users.OrderBy(x => x.Point).ToList();

                if (Users.Count != 0)
                    Users.Clear();

                foreach (var user in users)
                    Users.Add(user);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally 
            { 
                IsBusy = false;
                IsRefreshing = false;
            }
        }

        [RelayCommand]
        public async Task Back()
        {
            if (IsBusy)
                return;

            try
            {
                await Shell.Current.GoToAsync("..");
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                IsBusy = false;
            }

        }

    }
}
