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

namespace TicketModule.Services
{
    public class ApiUserService : IApiUserService
    {
        private string baseUrl = "https://localhost:7249/api/";

        public async Task<ApiResponse> RegisterAsync(RegisterViewModel model)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = client.PostAsJsonAsync("Account/Register", model).Result;
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

        public async Task<ApiResponse> LoginAsync(LoginViewModel model)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = client.PostAsJsonAsync("Account/Login", model).Result;
            var result = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                return new ApiResponse
                {
                    IsSuccess = false,
                    Message = result
                };
            }

            //TODO: Não está a passar o formato corretamente
            var token = new JwtSecurityToken(jwtEncodedString: result);

            return new ApiResponse
            {
                IsSuccess = true,
                Message = "Hello World!",
                Result = token
            };
        }
    }
}
