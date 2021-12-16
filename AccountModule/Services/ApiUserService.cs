﻿using AccountModule.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace AccountModule.Services
{
    public class ApiUserService : IApiUserService
    {
        private string baseUrl = "https://localhost:7249/api/";

        public async Task<UserResponse> RegisterAsync(RegisterViewModel model)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = client.PostAsJsonAsync("Account/Register", model).Result;
            var result = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                return new UserResponse
                {
                    IsSuccess = false,
                    Message = result
                };
            }

            return new UserResponse
            {
                IsSuccess = true,
                Message = "Your registration is complete, Check your email for more information."
            };
        }

        public async Task<UserResponse> LoginAsync(LoginViewModel model)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = client.PostAsJsonAsync("Account/Login", model).Result;
            var result = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                return new UserResponse
                {
                    IsSuccess = false,
                    Message = result
                };
            }

            var userInfo = JsonConvert.DeserializeObject<UserResponseViewModel>(result);

            return new UserResponse
            {
                IsSuccess = true,
                Message = $"Welcome {userInfo.FirstName}!",
                Result = userInfo
            };
        }
    }
}
