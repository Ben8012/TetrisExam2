using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TetrisExam.Appli.Model;

namespace TetrisExam.Appli.Service
{
    public interface IUserServiceSqlLite
    {
        public Task<User> Login(Login login);
        public Task<User> Register(Register register);
        public Task<User> Update(Update update);
        public Task<int> Delete(User user);

        public Task<List<User>> GetAllUsers();

        public Task Delete();
    }
}
