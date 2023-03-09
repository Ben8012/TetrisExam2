
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TetrisExam.Appli.Model;
using TetrisExam.Appli.Model.SqlLite;

namespace TetrisExam.Appli.Service
{
    public class UserServiceSqlLite : IUserServiceSqlLite
    {
        private string _dbPath;

        private SQLiteAsyncConnection conn;

        public UserServiceSqlLite(string dbPath)
        {
            _dbPath = dbPath;
        }

        private void Init()
        {
            if (conn != null)
                return;

            conn = new SQLiteAsyncConnection(_dbPath);
            conn.CreateTableAsync<UserSqlLite>();
        }

        public async Task<User> Login(Login login)
        {
            try
            {
                if (string.IsNullOrEmpty(login.Email) || string.IsNullOrEmpty(login.Password))
                    throw new Exception("Valid name, password, email required");

                User user = new User();
                return  user;

            }
            catch (Exception ex)
            {

                throw new Exception("Message", ex);
            }

        }


        public async Task<User> Register(Register register)
        {
            try
            {
                Init();
                int result = 0;
                // basic validation to ensure a name was entered
                if (string.IsNullOrEmpty(register.Name) || string.IsNullOrEmpty(register.Password) || string.IsNullOrEmpty(register.Email))
                    throw new Exception("Valid name, password, email required");

                // TODO: Insert the new person into the database
                result = await conn.InsertAsync(new UserSqlLite
                {
                    Name = register.Name,
                    Email = register.Email,
                    Password = register.Password,
                    Point = 0,
                    IsActive = true
                });

                User user = new User
                {
                    Name = register.Name,
                    Email = register.Email,
                    Point = 0,
                    Token = ""
                };

                return user;

            }
            catch (Exception ex)
            {
                throw new Exception("Message",ex);
            }
        }

        public async Task<List<User>> GetAllUsers()
        {
            try
            {
                Init();
                List<UserSqlLite> users = await conn.Table<UserSqlLite>().ToListAsync();

                List<User> list = new List<User>();

                foreach (UserSqlLite user in users)
                {
                    list.Add(new User
                    {
                        Id= user.Id,
                        Name = user.Name,
                        Email = user.Email,
                        Point = user.Point,
                        Token = ""
                    });
                }

                return list;

            }
            catch (Exception ex)
            {
                throw new Exception("Message", ex);
            }
        }



    }    

}
