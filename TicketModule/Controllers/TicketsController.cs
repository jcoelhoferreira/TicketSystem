﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicketModule.Models;
using TicketModule.Services;
using TicketModule.ViewModels;

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
        public IActionResult Index()
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
        public IActionResult Create()
        {
            return View();
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
                    var result = await _apiService.CreateApiTicket(ticket);
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
