using DataAccess.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicketAPI.ViewModels;

namespace TicketAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserHelper _userHelper;

        public AccountController(IUserHelper userHelper)
        {
            _userHelper = userHelper;
        }

        //// POST: api/Account
        //[HttpPost]
        //public async Task<IActionResult> Register(RegisterViewModel model)
        //{

        //}
    }
}
