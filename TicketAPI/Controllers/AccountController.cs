using DataAccess.Entities;
using DataAccess.Helpers;
using DataAccess.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TicketAPI.Services;

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

        // POST: api/Account/Register
        [HttpPost("Register")]
        public async Task<IActionResult> CreateUser(RegisterViewModel model)
        {
            var email = _decrypton.DecryptString(model.Email);
            var user = await _userHelper.GetUserByEmailAsync(email);

            if(user == null)
            {
                user = new User
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = email,
                    UserName = email,
                };

                var password = _decrypton.DecryptString(model.Password);
                var result = await _userHelper.AddUserAsync(user, password);

                await _userHelper.CheckRoleAsync("Client");
                await _userHelper.AddUserToRoleAsync(user, "Client");

                if (result != IdentityResult.Success)
                {
                    ModelState.AddModelError(string.Empty, "The user couldn't be created.");
                    return NotFound();
                }

                return Ok();
            }

            return NotFound();
        }

        // POST: api/Account/Login
        [HttpPost("Login")]
        public async Task<IActionResult> LoginUser(LoginViewModel model)
        {
            model.Email = _decrypton.DecryptString(model.Email);
            model.Password = _decrypton.DecryptString(model.Password);
            var user = await _userHelper.GetUserByEmailAsync(model.Email);

            if(user == null)
            {
                return NotFound();
            }

            var result = await _userHelper.LoginAsync(model);

            if (result.Succeeded)
            {
                var userResponse = new UserResponseViewModel
                {
                    FirstName = user.FirstName,
                    UserName = user.UserName,
                    Role = _userHelper.GetRoleAsync(user).Result
                };

                return Ok(userResponse);
            }

            return NotFound();
        }
    }
}
