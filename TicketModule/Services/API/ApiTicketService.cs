using Microsoft.AspNetCore.Http;
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

namespace TicketModule.Services.API
{
    public class ApiTicketService : IApiTicketService
    {
        private static string baseUrl = "https://localhost:7249/api/";

        public async Task<ApiResponse> GetAllTickets(string accessToken)
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

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

                var tickets = JsonConvert.DeserializeObject<List<Ticket>>(result).ToList();


                if (tickets.Count == 0)
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
                    Result = tickets
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

        public async Task<ApiResponse> GetApiTicket(int id, string accessToken)
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

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

                var ticket = JsonConvert.DeserializeObject<Ticket>(result);

                if (ticket == null)
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
                    Result = ticket
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

        public async Task<ApiResponse> CreateApiTicket(NewTicketViewModel ticket, string accessToken)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
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

        public async Task<ApiResponse> EditApiTicket(int id, TicketViewModel model, string accessToken)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = client.PutAsJsonAsync("Tickets/" + id, model).Result;
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
    }
}
