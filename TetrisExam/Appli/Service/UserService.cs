using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TetrisExam.Appli.Model;
using System.Net.Http.Json;
using System.Threading;
using TetrisExam.Appli.Model.SqlLite;
using SQLite;
using Microsoft.Maui.Networking;
using Microsoft.Win32;
using static SQLite.SQLite3;
using static System.Net.WebRequestMethods;

namespace TetrisExam.Appli.Service
{
    // Service permettant de se connecter l'api afin de faire un CRUD sur User
    public class UserService : IUserService 
    {
        private string _url = "https://localhost:7267/api/User";
      
        private HttpClient _httpClient;
        private JsonSerializerOptions _serializerOptions;
       
       


        public UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
           
            // configuration de la serealisation en JSON
            _serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };

        }

    

        public async Task<User> Login(Login login)
        {
            try
            {
                // Serealisation en JSON
                string json = JsonSerializer.Serialize(login, _serializerOptions);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                // envois vers api en POST
                var response = await _httpClient.PostAsync(_url + "/Login", content);

                // verification du status http pour renvoyer le resultat
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<User>();
                }
                return null;
                        
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
                // Serealisation en JSON
                string json = JsonSerializer.Serialize(register, _serializerOptions);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                // envois vers api en POST
                var response = await _httpClient.PostAsync(_url, content);

                // verification du status http pour renvoyer le resultat
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<User>();
                }
                
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Message", ex);
            }
        }

        public async Task<List<User>> GetAllUsers()
        {
            try
            {
                // recuperation des user en GET
                var response = await _httpClient.GetAsync(_url);

                // verification du status http pour renvoyer le resultat
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<User>>();
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Message", ex);
            }
        }

        public async Task<User> Update(Update update)
        {
            try
            {
                // Serealisation en JSON
                string json = JsonSerializer.Serialize(update, _serializerOptions);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                // envois vers api en PUT
                var response = await _httpClient.PutAsync(_url + "/" + update.Id, content);

                // verification du status http pour renvoyer le resultat
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<User>();
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Message", ex);
            }

        }


        public async Task Delete(int id)
        {
            try
            {
                // desactivation du USER => cette fonction desactive et non supprime dans le cas ou l'on voudrait reactiver un USER
                var response = await _httpClient.DeleteAsync(_url + "/" + id);
            }
            catch (Exception ex)
            {
                throw new Exception("Message", ex);
            }

        }
    }
}
