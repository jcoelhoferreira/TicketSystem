using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketModule.Models;
using TicketModule.ViewModels;

namespace TicketModule.Services.API
{
    public interface IApiTicketService
    {
        Task<ApiResponse> GetAllTickets(string accessToken);
        Task<ApiResponse> GetApiTicket(int id);
        Task<ApiResponse> CreateApiTicket(NewTicketViewModel ticket, string accessToken);
        Task<ApiResponse> EditApiTicket(Ticket ticket);
        Task<ApiResponse> DeleteApiTicket(int id);
    }
}
