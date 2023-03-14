using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TetrisExam.Appli.Model;

namespace TetrisExam.Appli.ViewModel;

[QueryProperty(nameof(User), "User")]
public partial class GameViewModel : BaseViewModel
{
    [ObservableProperty]
    User user;

    // cette classe est prevue pour le jeu a faire plus tard
}
