using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AccountModule.Services;
using AccountModule.ViewModels;

namespace AccountModule
{
    public class AccountController : Controller
    {
        private readonly IApiUserService _apiService;
        private readonly IEncryption _encryption;

        public AccountController(IApiUserService apiService, IEncryption encryption)
        {
            _apiService = apiService;
            _encryption = encryption;
        }

        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
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
                    var result = await _apiService.LoginAsync(model);
                    if (result.IsSuccess)
                    {
                        ViewBag.Message = result.Message;
                        return View();
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
                    var result = await _apiService.RegisterAsync(model);
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
