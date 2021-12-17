using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketModule.Models;
using TicketModule.ViewModels;

namespace TicketModule.Services
{
    public interface IApiTicketService
    {
        Task<ApiResponse> GetAllApiTickets();
        Task<ApiResponse> GetApiTicket(int id);
        Task<ApiResponse> CreateApiTicket(NewTicketViewModel ticket);
        Task<ApiResponse> EditApiTicket(Ticket ticket);
        Task<ApiResponse> DeleteApiTicket(int id);
        Task<ApiResponse> GetUserApiTickets(string username);
    }
}
