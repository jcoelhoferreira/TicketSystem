using DataAccess.Entities;
using DataAccess.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class TicketRepository : ITicketRepository
    {
        private readonly DataContext _dataContext;

        public TicketRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public void AddTicket(Ticket ticket)
        {
            _dataContext.Tickets.Add(ticket);
        }

        public IEnumerable<Ticket> GetAll()
        {
            return _dataContext.Tickets.ToList();
        }

        public IEnumerable<Ticket> GetAllWithUsers()
        {
            return _dataContext.Tickets
                .Include(t => t.UserInfo)
                .ToList();
        }

        public Ticket GetTicket(int id)
        {
            return _dataContext.Tickets.Find(id);
        }

        public void RemoveTicket(Ticket ticket)
        {
            _dataContext.Tickets.Remove(ticket);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _dataContext.SaveChangesAsync() > 0;
        }

        public bool TicketExists(int id)
        {
            return _dataContext.Tickets.Any(t => t.Id == id);
        }

        public void UpdateTicket(Ticket ticket)
        {
            _dataContext.Tickets.Update(ticket);
        }
    }
}
