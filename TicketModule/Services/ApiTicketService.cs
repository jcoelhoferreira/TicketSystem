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

        public async Task<TicketResponse> CreateApiTicket(NewTicketViewModel ticket)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = client.PostAsJsonAsync("Tickets", ticket).Result;
            var result = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                return new TicketResponse
                {
                    IsSuccess = false,
                    Message = result
                };
            }

            return new TicketResponse
            {
                IsSuccess = true,
                Message = "Your ticket was submitted successfuly. Our team is working on your issue!"
            };
        }

        public Task<TicketResponse> DeleteApiTicket(int id)
        {
            throw new NotImplementedException();
        }

        public Task<TicketResponse> EditApiTicket(Ticket ticket)
        {
            throw new NotImplementedException();
        }

        public async Task<TicketResponse> GetAllApiTickets()
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(baseUrl);

                var response = await client.GetAsync("Tickets");
                var result = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return new TicketResponse
                    {
                        IsSuccess = false,
                        Message = result
                    };
                }

                var apiInfo = JsonConvert.DeserializeObject<List<Ticket>>(result);

                if (apiInfo.Count == 0)
                {
                    return new TicketResponse
                    {
                        IsSuccess = false,
                        Message = result
                    };
                }

                return new TicketResponse
                {
                    IsSuccess = true,
                    Result = apiInfo
                };
            }
            catch (Exception e)
            {
                return new TicketResponse
                {
                    IsSuccess = false,
                    Message = e.Message
                };
            }
        }

        public async Task<TicketResponse> GetApiTicket(int id)
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(baseUrl);

                var response = await client.GetAsync("Tickets/" + id);
                var result = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return new TicketResponse
                    {
                        IsSuccess = false,
                        Message = result
                    };
                }

                var apiInfo = JsonConvert.DeserializeObject<Ticket>(result);

                if (apiInfo == null)
                {
                    return new TicketResponse
                    {
                        IsSuccess = false,
                        Message = result
                    };
                }

                return new TicketResponse
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
