using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using TicketModule.Models;
using TicketModule.ViewModels;

namespace TicketModule.Services
{
    public class ApiTicketService : IApiTicketService
    {
        private string baseUrl = "https://localhost:7249/api/";

        public async Task<ApiResponse> GetUserApiTickets(string username)
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(baseUrl);

                var response = await client.GetAsync("Tickets/" + username + "/ticket");
                var result = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return new ApiResponse
                    {
                        IsSuccess = false,
                        Message = result
                    };
                }

                var apiInfo = JsonConvert.DeserializeObject<List<Ticket>>(result);

                if (apiInfo.Count == 0)
                {
                    return new ApiResponse
                    {
                        IsSuccess = false,
                        Message = result
                    };
                }

                return new ApiResponse
                {
                    IsSuccess = true,
                    Result = apiInfo
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

        

        public async Task<ApiResponse> CreateApiTicket(NewTicketViewModel ticket)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = client.PostAsJsonAsync("Tickets", ticket).Result;
            var result = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                return new ApiResponse
                {
                    IsSuccess = false,
                    Message = result
                };
            }

            return new ApiResponse
            {
                IsSuccess = true,
                Message = "Your ticket was submitted successfuly. Our team is working on your issue!"
            };
        }

        public Task<ApiResponse> DeleteApiTicket(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse> EditApiTicket(Ticket ticket)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResponse> GetAllApiTickets()
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(baseUrl);

                var response = await client.GetAsync("Tickets");
                var result = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return new ApiResponse
                    {
                        IsSuccess = false,
                        Message = result
                    };
                }

                var apiInfo = JsonConvert.DeserializeObject<List<Ticket>>(result);

                if (apiInfo.Count == 0)
                {
                    return new ApiResponse
                    {
                        IsSuccess = false,
                        Message = result
                    };
                }

                return new ApiResponse
                {
                    IsSuccess = true,
                    Result = apiInfo
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

        public async Task<ApiResponse> GetApiTicket(int id)
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(baseUrl);

                var response = await client.GetAsync("Tickets/" + id);
                var result = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return new ApiResponse
                    {
                        IsSuccess = false,
                        Message = result
                    };
                }

                var apiInfo = JsonConvert.DeserializeObject<Ticket>(result);

                if (apiInfo == null)
                {
                    return new ApiResponse
                    {
                        IsSuccess = false,
                        Message = result
                    };
                }

                return new ApiResponse
                {
                    IsSuccess = true,
                    Result = apiInfo
                };

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
