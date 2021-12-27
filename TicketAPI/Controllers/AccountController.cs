using DataAccess;
using DataAccess.Entities;
using DataAccess.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace TicketAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly DataContext _context;

        public AccountController(DataContext context)
        {
            _context = context;
        }

        // GET: api/UsersInfo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserInfo>>> GetUserInfo()
        {
            return await _context.UsersInfo.ToListAsync();
        }

        // GET: api/UsersInfo/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserInfo>> GetUserInfo(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.UsersInfo
                .FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        // PUT: api/UsersInfo/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserInfo(int id, UserInfo userInfo)
        {
            if (id != userInfo.Id)
            {
                return BadRequest();
            }

            _context.Entry(userInfo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserInfoExists(id))
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

        // POST: api/UsersInfo
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostUserInfo(RegisterViewModel userInfo)
        {
            var user = new UserInfo
            {
                FirstName = userInfo.FirstName,
                LastName = userInfo.LastName,
                Username = userInfo.Email,
                Password = userInfo.Password,
                Role = "Client"
                
            };
            _context.UsersInfo.Add(user);
            await _context.SaveChangesAsync();

            return Ok();
        }


        // POST: UsersInfo/Delete/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<UserInfo>> DeleteUserInfo(int id)
        {
            var userInfo = await _context.UsersInfo.FindAsync(id);
            if (userInfo == null)
            {
                return NotFound();
            }

            _context.UsersInfo.Remove(userInfo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserInfoExists(int id)
        {
            return _context.UsersInfo.Any(e => e.Id == id);
        }
    }
}
