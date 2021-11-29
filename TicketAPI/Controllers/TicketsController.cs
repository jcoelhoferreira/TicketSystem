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

namespace TicketAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private readonly IRepository _repository;

        public TicketsController(IRepository repository)
        {
            _repository = repository;
        }

        // GET: api/Tickets
        [HttpGet]
        public ActionResult<IEnumerable<Ticket>> GetTickets()
        {
            return _repository.GetAll().ToList();
        }

        // GET: api/Tickets/5
        [HttpGet("{id}")]
        public ActionResult<Ticket> GetTicket(int id)
        {
            var ticket = _repository.GetTicket(id);

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
                _repository.UpdateTicket(ticket);
                await _repository.SaveAllAsync();
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
        public async Task<ActionResult<Ticket>> PostTicket(Ticket ticket)
        {
            _repository.AddTicket(ticket);
            await _repository.SaveAllAsync();

            return CreatedAtAction("GetTicket", new { id = ticket.Id }, ticket);
        }

        // DELETE: api/Tickets/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTicket(int id)
        {
            var ticket = _repository.GetTicket(id);
            if (ticket == null)
            {
                return NotFound();
            }

            _repository.RemoveTicket(ticket);
            await _repository.SaveAllAsync();

            return NoContent();
        }

        private bool TicketExists(int id)
        {
            return _repository.TicketExists(id);
        }
    }
}
