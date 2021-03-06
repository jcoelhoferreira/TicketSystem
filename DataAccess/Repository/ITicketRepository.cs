using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface ITicketRepository
    {
        void AddTicket(Ticket ticket);
        Ticket GetTicket(int id);
        IEnumerable<Ticket> GetAll();
        IEnumerable<Ticket> GetAllWithUsers();
        void RemoveTicket(Ticket ticket);
        Task<bool> SaveAllAsync();
        void UpdateTicket(Ticket ticket);
        bool TicketExists(int id);
    }
}
