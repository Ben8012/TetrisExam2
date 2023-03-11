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
        public Mock<IUserServiceSqlLite> mock2 = new Mock<IUserServiceSqlLite>();

        // untiliser Assert

        [Fact]
        public async Task TestGetBestScore()
        {
            List<User> Users = new List<User>();
          
            try
            {
               
                mock.Setup(m => m.GetAllUsers()).ReturnsAsync(Users);
                BestScoreViewModel vm = new BestScoreViewModel(mock.Object,mock2.Object);
                
                await vm.GetBestScore();
               
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [Fact]
        public async Task TestLogin()
        {
            User user = new User();
            Login login = new Login();
            login.Email = "tim@mail.com";
            login.Password = "test1234=";
            try
            {

                mock.Setup(m => m.Login(login)).ReturnsAsync(user);
                LoginViewModel vm = new LoginViewModel(mock.Object);

                await vm.Login();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}