using DataAccess.Entities;
using DataAccess.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TicketAPI.Services;
using TicketAPI.ViewModels;

namespace TicketAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserHelper _userHelper;
        private readonly IDecryption _decrypton;

        public AccountController(IUserHelper userHelper, IDecryption decrypton)
        {
            _userHelper = userHelper;
            _decrypton = decrypton;
        }

        // POST: api/Account
        [HttpPost]
        public async Task<IActionResult> PostUser(RegisterViewModel model)
        {
            var user = await _userHelper.GetUserByEmailAsync(model.Email);

            if(user == null)
            {
                user = new User
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    UserName = model.Email,
                };

                var result = await _userHelper.AddUserAsync(user, _decrypton.DecryptString(model.Password));
                if (result != IdentityResult.Success)
                {
                    ModelState.AddModelError(string.Empty, "The user couldn't be created.");
                    return NotFound();
                }

                return Ok();
            }

            return NotFound();
        }
    }
}
