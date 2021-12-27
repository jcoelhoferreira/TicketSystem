using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using TicketModule.Models;
using TicketModule.Services;
using TicketModule.Services.API;
using TicketModule.ViewModels;

namespace TicketModule
{
    public class AccountController : Controller
    {
        private readonly IApiUserService _apiUserService;
        private readonly IEncryption _encryption;

        public AccountController(IApiUserService apiUserService, IEncryption encryption)
        {
            _apiUserService = apiUserService;
            _encryption = encryption;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _apiUserService.RegisterAsync(model);
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

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel userInfo)
        {
            if (ModelState.IsValid)
            {
                var result = await _apiUserService.LoginUserAsync(userInfo);

                if (!result.IsSuccess)
                {
                    ViewBag.Message = "Incorrect Username or Password";
                    return RedirectToAction("Index", "Home");
                    //return Redirect("~/Home/Index");
                }
                var token = result.Message;
                HttpContext.Session.SetString("JWToken", token);
            }
            return Redirect("~/TicketModule/Tickets/Index");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
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
