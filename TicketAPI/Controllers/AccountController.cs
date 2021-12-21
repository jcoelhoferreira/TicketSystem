using DataAccess.Entities;
using DataAccess.Helpers;
using DataAccess.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TicketAPI.Services;

namespace TicketAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserHelper _userHelper;
        private readonly IDecryption _decrypton;
        private readonly IConfiguration _configuration;

        public AccountController(IUserHelper userHelper, IDecryption decrypton, IConfiguration configuration)
        {
            _userHelper = userHelper;
            _decrypton = decrypton;
            _configuration = configuration;
        }

        // POST: api/Account/Register
        [HttpPost("register")]
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
                    return BadRequest();
                }

                return Ok();
            }

            return BadRequest();
        }

        // POST: api/Account/Login
        [HttpPost("login")]
        public async Task<IActionResult> LoginUser(LoginViewModel model)
        {
            var userInfo = new LoginViewModel
            {
                Email = _decrypton.DecryptString(model.Email),
                Password = _decrypton.DecryptString(model.Password),
                RememberMe = model.RememberMe
            };

            var user = await _userHelper.GetUserByEmailAsync(userInfo.Email);

            if(user == null)
            {
                return NotFound();
            }

            var result = await _userHelper.LoginAsync(userInfo);

            if (result.Succeeded)
            {
                string token = await CreateToken(user);
                return Ok(token);
                //var userResponse = new UserResponseViewModel
                //{
                //    FirstName = user.FirstName,
                //    UserName = user.UserName,
                //    Role = _userHelper.GetRoleAsync(user).Result
                //};

                //return Ok(userResponse);
            }

            return Unauthorized();
        }

        private async Task<string> CreateToken(User user)
        {
            var userRole = await _userHelper.GetRoleAsync(user);

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.FullName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role, userRole)
            };

            var authSigninKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Tokens:Key"]));

            var token = new JwtSecurityToken(
                    issuer: _configuration["Tokens:Issuer"],
                    audience: _configuration["Tokens:Audience"],
                    expires: DateTime.Now.AddHours(24),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigninKey, SecurityAlgorithms.HmacSha256)
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}
