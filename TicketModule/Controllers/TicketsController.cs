using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicketModule.Services;

namespace TicketModule
{
    public class TicketsController : Controller
    {
        private readonly IApiService _apiService;

        public TicketsController(IApiService apiService)
        {
            _apiService = apiService;
        }

        // GET: TicketsController
        public ActionResult Index()
        {
            var result = _apiService.GetAllApiTickets().Result.Result;

            return View(result);
        }

        // GET: TicketsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TicketsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TicketsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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

        // GET: TicketsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TicketsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
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
