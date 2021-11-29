using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class SeedDb
    {
        private readonly DataContext _dataContext;

        public SeedDb(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task SeedAsync()
        {
            await _dataContext.Database.EnsureCreatedAsync();

            if (!_dataContext.Tickets.Any())
            {
                AddTicket("Não consigo fazer login",
                    "Não consigo fazer login na aplicação." +
                    " Gostava que me ajudassem o mais ráido possível",
                    false);
                AddTicket("O bluetooth não funciona",
                    "Não consigo sincronizar os aparelhos." +
                    " Gostava que me ajudassem o mais ráido possível",
                    false);
                AddTicket("A aplicação crasha no arranque",
                    "A aplicação não arranca." +
                    " Gostava que me ajudassem o mais ráido possível",
                    false);
                await _dataContext.SaveChangesAsync();
            }

        }

        private void AddTicket(string title, string description, bool isSolved)
        {
            _dataContext.Tickets.Add(new Ticket
            {
                Title = title,
                Description = description,
                IsSolved = isSolved
            });
        }
    }
}
