using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using TicketModule.Services;
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
                model.Email = _encryption.EncryptString(model.Email);
                model.Password = _encryption.EncryptString(model.Password);
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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.Email = _encryption.EncryptString(model.Email);
                model.Password = _encryption.EncryptString(model.Password);
                try
                {
                    var result = await _apiUserService.LoginAsync(model);
                    if (result.IsSuccess)
                    {
                        var token = (JwtSecurityToken)result.Result;
                        var userRole = token.Claims.First(c => c.Type == "Role").Value;

                        if (userRole == "Client")
                        {
                            return RedirectToAction("IndexClient", "Tickets");
                        }

                        return RedirectToAction("Index", "Tickets");

                        ViewBag.Message = result.Message;
                        return RedirectToAction("Index","Home");
                    }
                    
                }
                catch (Exception e)
                {
                    ViewBag.Message = e.Message.ToString();
                }
            }

            this.ModelState.AddModelError(string.Empty, "Failed to login!");
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
