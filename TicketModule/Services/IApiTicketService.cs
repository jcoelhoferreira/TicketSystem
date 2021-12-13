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
        Task<TicketResponse> GetAllApiTickets();
        Task<TicketResponse> GetApiTicket(int id);
        Task<TicketResponse> CreateApiTicket(NewTicketViewModel ticket);
        Task<TicketResponse> EditApiTicket(Ticket ticket);
        Task<TicketResponse> DeleteApiTicket(int id);
    }
}
