using DataAccess;
using DataAccess.Entities;
using DataAccess.Helpers;
using DataAccess.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace TicketAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly DataContext _dataContext;

        public TokenController(IConfiguration configuration, DataContext dataContext)
        {
            _configuration = configuration;
            _dataContext = dataContext;
        }

        [HttpPost]
        public async Task<IActionResult> Post(LoginViewModel userInfo)
        {
            if(userInfo != null && userInfo.Username != null && userInfo.Password != null)
            {
                var user = await GetUser(userInfo.Username, userInfo.Password);
                if(user != null)
                {
                    var claims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub,_configuration["Tokens:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat,DateTime.UtcNow.ToString()),
                        new Claim("Id", user.Id.ToString()),
                        new Claim(ClaimTypes.Name, user.Username),
                        new Claim(ClaimTypes.Role, user.Role),
                    };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Tokens:Key"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                        _configuration["Tokens:Issuer"],
                        _configuration["Tokens:Audience"],
                        claims,
                        expires: DateTime.Now.AddMinutes(20),
                        signingCredentials: signIn);

                    return Ok(new JwtSecurityTokenHandler().WriteToken(token));
                }
            }
            return BadRequest();
        }

        [HttpGet]
        public async Task<UserInfo> GetUser(string username, string password)
        {
            return await _dataContext.UsersInfo.FirstOrDefaultAsync(u => u.Username == username && u.Password == password);
        }
    }
}
