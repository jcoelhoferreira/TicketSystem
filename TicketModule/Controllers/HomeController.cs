using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicketModule.Services.API;
using TicketModule.ViewModels;

namespace TicketModule.Controllers
{
    public class HomeController : Controller
    {
        private readonly IApiUserService _apiUserService;

        public HomeController(IApiUserService apiUserService)
        {
            _apiUserService = apiUserService;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
