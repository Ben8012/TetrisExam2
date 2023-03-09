using Moq;
using System.Security.Cryptography.X509Certificates;
using TetrisExam.Appli.Model;
using TetrisExam.Appli.Service;
using TetrisExam.Appli.ViewModel;

namespace TetrisExamen.Test
{
    
    public class UnitTest1 
    {
        public Mock<IUserService> mock = new Mock<IUserService>();

       

        [Fact]
        public async Task TestGetBestScore()
        {
            List<User> Users = new List<User>();
            try
            {
                mock.Setup(m => m.GetAllUsers()).ReturnsAsync(Users);
                BestScoreViewModel vm = new BestScoreViewModel(mock.Object);
                await vm.GetBestScore();
               
            }
            catch (Exception ex)
            {

                throw ex;
            }
           

        }
    }
}