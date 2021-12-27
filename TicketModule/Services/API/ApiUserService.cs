using TicketModule.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using TicketModule.Models;

namespace TicketModule.Services.API
{
    public class ApiUserService : IApiUserService
    {
        private string baseUrl = "https://localhost:7249/api/";

        public async Task<ApiResponse> RegisterAsync(RegisterViewModel model)
         {
            var client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = client.PostAsJsonAsync("Account", model).Result;
            var result = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                return new ApiResponse
                {
                    IsSuccess = false,
                    Message = "This Username already exists! Try with a different username!"
                };
            }

            return new ApiResponse
            {
                IsSuccess = true,
                Message = "Registration successful!"
            };
        }

        public async Task<ApiResponse> LoginUserAsync(LoginViewModel userInfo)
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = client.PostAsJsonAsync("Token", userInfo).Result;
                string token = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return new ApiResponse
                    {
                        IsSuccess = false,
                        Message = token
                    };
                }

                token = token.Substring(1, token.Length - 2);

                return new ApiResponse
                {
                    IsSuccess = true,
                    Message = token
                };
                    
            }
            catch (Exception e)
            {
                return new ApiResponse
                {
                    IsSuccess = false,
                    Message = e.Message
                };
            }
        }
    }
}
