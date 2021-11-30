using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketModule.Models;

namespace TicketModule.Services
{
    public interface IApiService
    {
        Task<Response> GetAllApiTickets();
        Task<Response> GetApiTicket(int id);
        Task<Response> CreateApiTicket(Ticket ticket);
        Task<Response> EditApiTicket(Ticket ticket);
        Task<Response> DeleteApiTicket(int id);
    }
}
