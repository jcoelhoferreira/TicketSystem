using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataAccess;
using DataAccess.Entities;
using DataAccess.Repository;
using TicketAPI.Services;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using DataAccess.ViewModels;

namespace TicketAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TicketsController : ControllerBase
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly DataContext _dataContext;

        public TicketsController(ITicketRepository ticketRepository, DataContext dataContext)
        {
            _ticketRepository = ticketRepository;
            _dataContext = dataContext;
        }

        // GET: api/Tickets
        [HttpGet]
        public ActionResult<IEnumerable<Ticket>> GetTickets()
        {
            if(User.FindFirstValue(ClaimTypes.Role) != "Admin")
            {
                return Unauthorized();
            }
            return _ticketRepository.GetAllWithUsers().ToList();
        }

        // GET: api/Tickets/5
        [HttpGet("{id}")]
        public ActionResult<Ticket> GetTicket(int id)
        {
            var ticket = _ticketRepository.GetTicket(id);

            if (ticket == null)
            {
                return NotFound();
            }

            return ticket;
        }

        // PUT: api/Tickets/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTicket(int id, Ticket ticket)
        {
            if (id != ticket.Id)
            {
                return BadRequest();
            }

            try
            {
                _ticketRepository.UpdateTicket(ticket);
                await _ticketRepository.SaveAllAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TicketExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Tickets
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Ticket>> PostTicket(NewTicketViewModel modelTicket)
        {
            var user = User.Identity.Name;
            var client = await _dataContext.UsersInfo.FirstOrDefaultAsync(u => u.Username == user);

            var newTicket = new Ticket
            {
                Title = modelTicket.Title,
                Description = modelTicket.Description,
                UserInfo = client
            };

            _ticketRepository.AddTicket(newTicket);
            await _ticketRepository.SaveAllAsync();

            return Ok();
            /*return CreatedAtAction("GetTickets", new { id = newTicket.Id }, newTicket)*/;
        }

        // DELETE: api/Tickets/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTicket(int id)
        {
            var ticket = _ticketRepository.GetTicket(id);
            if (ticket == null)
            {
                return NotFound();
            }

            _ticketRepository.RemoveTicket(ticket);
            await _ticketRepository.SaveAllAsync();

            return NoContent();
        }

        private bool TicketExists(int id)
        {
            return _ticketRepository.TicketExists(id);
        }
    }
}
