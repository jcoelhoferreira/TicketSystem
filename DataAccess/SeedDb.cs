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

            await _userHelper.CheckRoleAsync("Admin");
            await _userHelper.CheckRoleAsync("Client");

            var user = await _userHelper.GetUserByEmailAsync("ticketsadmin@yopmail.com");
            var user2 = await _userHelper.GetUserByEmailAsync("luisclient@yopmail.com");
            
            //add user admin
            if (user == null)
            {
                user = new User
                {
                    FirstName = "Fernando",
                    LastName = "Pessoa",
                    UserName = "ticketsadmin@yopmail.com",
                    Email = "ticketsadmin@yopmail.com"
                };
            }

            var result = await _userHelper.AddUserAsync(user, "123456");
            if(result != IdentityResult.Success)
            {
                throw new InvalidOperationException("Could not create the user in seeder");
            }

            await _userHelper.AddUserToRoleAsync(user, "Admin");

            var isInRole = await _userHelper.IsUserInRoleAsync(user, "Admin");
            if (!isInRole)
            {
                await _userHelper.AddUserToRoleAsync(user, "Admin");
            }

            //add user client
            if (user2 == null)
            {
                user2 = new User
                {
                    FirstName = "Luis",
                    LastName = "Camoes",
                    UserName = "luisclient@yopmail.com",
                    Email = "luisclient@yopmail.com"
                };
            }

            var result2 = await _userHelper.AddUserAsync(user2, "123456");
            if (result2 != IdentityResult.Success)
            {
                throw new InvalidOperationException("Could not create the user in seeder");
            }

            await _userHelper.AddUserToRoleAsync(user2, "Client");

            var isInRole2 = await _userHelper.IsUserInRoleAsync(user2, "Client");
            if (!isInRole2)
            {
                await _userHelper.AddUserToRoleAsync(user2, "Client");
            }

            //add tickets
            if (!_dataContext.Tickets.Any())
            {
                AddTicket("Não consigo fazer login",
                    "Não consigo fazer login na aplicação." +
                    " Gostava que me ajudassem o mais ráido possível",
                    false,
                    user);
                AddTicket("O bluetooth não funciona",
                    "Não consigo sincronizar os aparelhos." +
                    " Gostava que me ajudassem o mais ráido possível",
                    false,
                    user);
                AddTicket("A aplicação crasha no arranque",
                    "A aplicação não arranca." +
                    " Gostava que me ajudassem o mais ráido possível",
                    false,
                    user);
                await _dataContext.SaveChangesAsync();
            }
        }

        private void AddTicket(string title, string description, bool isSolved, User user)
        {
            _dataContext.Tickets.Add(new Ticket
            {
                Title = title,
                Description = description,
                IsSolved = isSolved,
                User = user
            });
        }
    }
}
