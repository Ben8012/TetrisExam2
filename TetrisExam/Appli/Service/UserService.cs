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

namespace TetrisExam.Appli.Service
{
    public class UserService : IUserService 
    {
        private string _url = "https://localhost:7267/api/User";
      
        private HttpClient _httpClient;
        private JsonSerializerOptions _serializerOptions;
       
       


        public UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
           
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
                string json = JsonSerializer.Serialize(login, _serializerOptions);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(_url + "/Login", content);

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
               
                    string json = JsonSerializer.Serialize(register, _serializerOptions);
                    StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = await _httpClient.PostAsync(_url, content);

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

                var response = await _httpClient.GetAsync(_url);

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
       
    }
}
