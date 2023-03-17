using Moq;
using System.Security.Cryptography.X509Certificates;
using TetrisExam.Appli.Model;
using TetrisExam.Appli.Service;
using TetrisExam.Appli.ViewModel;
using Plugin.Connectivity.Abstractions;
using Xunit;
// using Microsoft.Maui.Essentials;

namespace TetrisExamen.Test
{
    
    public class UnitTest1 
    {
        public Mock<IUserService> mock = new Mock<IUserService>();
        public Mock<IUserServiceSqlLite> mock2 = new Mock<IUserServiceSqlLite>();
        public Mock<IConnectivity> mock3 = new Mock<IConnectivity>();

        public Mock<HttpClient> mockHttpClient = new Mock<HttpClient>();

        // untiliser Assert

        [Fact]
        public async Task TestGetBestScore()
        {
            List<User> Users = new List<User>();
          
            try
            {
               
                mock.Setup(m => m.GetAllUsers()).ReturnsAsync(Users);
                //BestScoreViewModel vm = new BestScoreViewModel(
                //    mock.Object,
                //    mock2.Object,
                //    mock3.Object
                //    );
                
                //await vm.GetBestScore();
               
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
                //LoginViewModel vm = new LoginViewModel(
                //    mock.Object,
                //    mock2.Object,
                //   mock3.Object
                //    );

                //await vm.Login();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        [Theory]
        [InlineData("luc@mail.com", "test1234=")]   // Assert.True sera true
        [InlineData("tim@mail.com", "test1234=")]   // tim n'est plus actif dans ma db renvois un user null d'ou echec
        [InlineData("test@mail.com", "test1234=")]  // Assert.True sera false, les email ne sont pas egaux
        public async Task TestLogin2(string email, string password)
        {
            User user = new User();
            Login login = new Login();
            login.Email = email;
            login.Password = password;
            try
            {

                HttpClient client = new HttpClient();
                UserService userService = new UserService(client);

                user = await userService.Login(login);

                // Assert.Contains(user.Email,"tim@mail.com");  // compare si contenu dans une collection
                // Assert.Same(user.Email, "luc@mail.com");     // pq ceci ne fonctionne pas     
                
                Assert.True(user.Email == "luc@mail.com");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [Fact]
        public async Task TestLogin3()
        {
            Login login = new Login();
            login.Email = "luc@mail.com";
            login.Password = "test1234=";
            try
            {

                HttpClient client = new HttpClient();
                UserService userService = new UserService(client);

                User user = await userService.Login(login);
 
                Assert.True(user.Email == "luc@mail.com");
                Assert.True(user.Name == "Luc");

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}