using DataAccess.Entities;
using DataAccess.Helpers;
using Microsoft.AspNetCore.Identity;
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
        private readonly IUserHelper _userHelper;

        public SeedDb(DataContext dataContext, IUserHelper userHelper)
        {
            _dataContext = dataContext;
            _userHelper = userHelper;
        }

        public async Task SeedAsync()
        {
            await _dataContext.Database.EnsureCreatedAsync();


            var user = await _userHelper.GetUserByEmailAsync("ticketsadmin@yopmail.com");
            var user2 = await _userHelper.GetUserByEmailAsync("luisclient@yopmail.com");

            //add user admin
            if (user == null)
            {
                user = new UserInfo
                {
                    FirstName = "Fernando",
                    LastName = "Pessoa",
                    Username = "ticketsadmin@yopmail.com",
                    Role = "Admin",
                    Password = "123456"
                };

                _userHelper.AddUserAsync(user);
                await _userHelper.SaveAllAsync();
            }

            //add user client
            if (user2 == null)
            {
                user2 = new UserInfo
                {
                    FirstName = "Luis",
                    LastName = "Camoes",
                    Username = "luisclient@yopmail.com",
                    Role = "Client",
                    Password = "123456"
                };

                _userHelper.AddUserAsync(user2);
                await _userHelper.SaveAllAsync();
            }

            //add tickets
            if (!_dataContext.Tickets.Any())
            {
                AddTicket("Não consigo fazer login",
                    "Não consigo fazer login na aplicação." +
                    " Gostava que me ajudassem o mais rápido possível",
                    false,
                    user2);
                AddTicket("O bluetooth não funciona",
                    "Não consigo sincronizar os aparelhos." +
                    " Gostava que me ajudassem o mais rápido possível",
                    false,
                    user2);
                AddTicket("A aplicação crasha no arranque",
                    "A aplicação não arranca." +
                    " Gostava que me ajudassem o mais rápido possível",
                    false,
                    user2);
                await _dataContext.SaveChangesAsync();
            }
        }

        private void AddTicket(string title, string description, bool isSolved, UserInfo user)
        {
            _dataContext.Tickets.Add(new Ticket
            {
                Title = title,
                Description = description,
                IsSolved = isSolved,
                UserInfo = user
            });
        }
    }
}
