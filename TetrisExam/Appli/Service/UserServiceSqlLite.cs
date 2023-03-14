
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

        // fonction pour initialisé la connection avec SqlLit
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
                //verification des remplis 
                if (string.IsNullOrEmpty(login.Email) || string.IsNullOrEmpty(login.Password))
                    throw new Exception("Valid name, password, email required");

                //recuperation du USER par son email
                UserSqlLite userSqlLite = await conn.GetAsync<UserSqlLite>(user => user.Email == login.Email);

                // prevu si nous renvoyons le mote de passe haché de la DB ici ce n'est pas le cas
                // bool verified = BCrypt.Net.BCrypt.Verify(login.Password, userSqlLite.Password);

                //if (verified)
                //{

                //mapping de UserSqlLite vers User pour renvoyer le bon type 
                User user = new User
                {
                        Id = userSqlLite.Id,
                        Name = userSqlLite.Name,
                        Email = userSqlLite.Email,
                        Point = userSqlLite.Point,
                        IsActive = userSqlLite.IsActive,
                        Token = ""
                    };
                    return user;
                //}

                //return null;
                

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
                    Point =  register.Point,
                    IsActive= register.IsActive,

                });

                //mapping de UserSqlLite vers User pour renvoyer le bon type
                User user = new User
                {
                    Name = register.Name,
                    Email = register.Email,
                    Point = 0,
                    IsActive = register.IsActive,
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
                // recuperation des tout les users dans SqlLite
                List<UserSqlLite> users = await conn.Table<UserSqlLite>().ToListAsync();

                // mapping de la list des UserSqlLite en list de User afin de renvoyer le bon type de liste
                List<User> list = new List<User>();
                foreach (UserSqlLite user in users)
                {
                    list.Add(new User
                    {
                        Id= user.Id,
                        Name = user.Name,
                        Email = user.Email,
                        Point = user.Point,
                        IsActive= user.IsActive,
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

        public async Task<User> Update(Update update)
        {
            Init();
            
            // mapping de Update vers  UserSqlLite inserer le bon type 
            UserSqlLite userSqlLite = new UserSqlLite
            {
                Id= update.Id,
                Name = update.Name,
                Email = update.Email,
            };

            //  modification
            await conn.UpdateAsync(userSqlLite);

            // recuperation du user modifié
            UserSqlLite userSqlLite2 = await conn.GetAsync<UserSqlLite>(user => user.Email == update.Email);

            // mapping de UserSqlLite en User pour renvoyer le bon type
            User user = new User
            {
                Name = userSqlLite2.Name,
                Email = userSqlLite2.Email,
                Point = userSqlLite2.Point,
                IsActive = userSqlLite2.IsActive,
                Token = ""
            };

            return user;
        }

        public async Task<int> Delete(User user)
        {
            Init();
            // mapping de User en UserSqlLite pour effacer le user dans SqlLite 
            UserSqlLite userSqlLite = new UserSqlLite()
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Point = user.Point,
                IsActive = user.IsActive
            };

            // suppression dans SqlLte

           return  await conn.DeleteAsync(userSqlLite);
        }

    
        public async Task Delete()
        {
            Init();
            // suppression de tout les users dans Sqlite
            await conn.DeleteAllAsync<UserSqlLite>();
           
        }

      
    }    

}
