using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AccountModule.Services;
using AccountModule.ViewModels;

namespace AccountModule
{
    public class AccountController : Controller
    {
        private readonly IApiUserService _apiService;

        public AccountController(IApiUserService apiService)
        {
            _apiService = apiService;
        }

        //// GET: AccountController
        //public IActionResult Index()
        //{
        //    var result = _apiService.GetAllApiTickets().Result.Result;

        //    return View(result);
        //}

        //// GET: TicketsController/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        // GET: TicketsController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TicketsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _apiService.RegisterAsync(user);
                    ViewBag.Message = result.Message;
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

        //// GET: TicketsController/Edit/5
        //public IActionResult Edit(int? id)
        //{
        //    if(id == null)
        //    {
        //        return NotFound();
        //    }

        //    var model = _apiService.GetApiTicket(id.Value).Result.Result;

        //    return View(model);
        //}

        //// POST: TicketsController/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: TicketsController/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: TicketsController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
