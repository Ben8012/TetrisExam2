
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
            Init();
            try
            {
                if (string.IsNullOrEmpty(login.Email) || string.IsNullOrEmpty(login.Password))
                    throw new Exception("Valid name, password, email required");

                // A faire 
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
                // Validation du champ nom et email
                if (string.IsNullOrEmpty(register.Name)  || string.IsNullOrEmpty(register.Email))
                    throw new Exception("Valid name, email required");

                // insertion dans sqlLite
                result = await conn.InsertAsync(new UserSqlLite
                {
                    Name = register.Name,
                    Email = register.Email,
                    Password = register.Password,
                    Point =  register.Point <= 0 ?  0 : register.Point

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

        public Task<User> Update(Update update)
        {
            Init();
            // A faire !!
            throw new NotImplementedException();
        }

        public async Task<int> Delete(User user)
        {
            Init();
            UserSqlLite userSqlLite = new UserSqlLite()
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Point = user.Point,
                IsActive = user.IsActive
            };

           return  await conn.DeleteAsync(userSqlLite);
        }

    
        public async Task Delete()
        {
            Init();
            await conn.DeleteAllAsync<UserSqlLite>();
           
        }

      
    }    

}
