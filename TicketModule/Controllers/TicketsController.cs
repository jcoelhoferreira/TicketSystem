using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicketModule.Models;
using TicketModule.Services.API;
using TicketModule.ViewModels;

namespace TicketModule
{
    public class TicketsController : Controller
    {
        private readonly IApiTicketService _apiTicketService;

        public TicketsController(IApiTicketService apiTicketService)
        {
            _apiTicketService = apiTicketService;
        }

        // GET: TicketsController
        public IActionResult Index()
        {
            var accessToken = HttpContext.Session.GetString("JWToken");

            if(accessToken != null)
            {
                var result = _apiTicketService.GetAllTickets(accessToken).Result;

                if (result.IsSuccess)
                {
                    var tickets = (List<Ticket>)result.Result;
                    return View(tickets);
                }

                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Login", "Account");
        }


        // GET: TicketsController/Details/5
        public ActionResult Details(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var accessToken = HttpContext.Session.GetString("JWToken");
            if (accessToken != null)
            {
                var ticket = _apiTicketService.GetApiTicket(id.Value, accessToken).Result.Result;
                if (ticket != null)
                {
                    return View(ticket);
                }
                return NotFound();
            }
            return RedirectToAction("Index");
        }

        // GET: TicketsController/Create
        public IActionResult Create()
        {
            var accessToken = HttpContext.Session.GetString("JWToken");
            if (accessToken != null)
            {
                return View();
            }
            return RedirectToAction("Login", "Account");
        }

        // POST: TicketsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(NewTicketViewModel ticket)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var accessToken = HttpContext.Session.GetString("JWToken");
                    if (accessToken != null)
                    {
                        var result = await _apiTicketService.CreateApiTicket(ticket, accessToken);
                        ViewBag.Message = result.Message;
                    }
                }
                catch (Exception e)
                {
                    ViewBag.Message = e.Message.ToString();
                }
                return View();
            }
            ViewBag.Error = "Invalid values, please try again.";
            return View();
        }

        // GET: TicketsController/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var accessToken = HttpContext.Session.GetString("JWToken");
            if(accessToken != null)
            {
                var ticket = (Ticket)_apiTicketService.GetApiTicket(id.Value, accessToken).Result.Result;

                var model = new TicketViewModel
                {
                    Id = ticket.Id,
                    Title = ticket.Title,
                    Description = ticket.Description,
                    Resolution = ticket.Resolution,
                    IsSolved = ticket.IsSolved,
                };

                return View(model);
            }
            return RedirectToAction("Index");
        }

        // POST: TicketsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(TicketViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var accessToken = HttpContext.Session.GetString("JWToken");
                    if (accessToken != null)
                    {
                        var result = await _apiTicketService.EditApiTicket(model.Id, model, accessToken);
                        ViewBag.Message = result.Message;
                    }
                }
                catch (Exception e)
                {
                    ViewBag.Message = e.Message.ToString();
                }
                return RedirectToAction("Index");
            }
            ViewBag.Error = "Invalid values, please try again.";
            return View();
        }

        // GET: TicketsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TicketsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
