using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WorkForBHHC.Data;
using WorkForBHHC.Data.Entities;
using WorkForBHHC.Models;

namespace WorkForBHHC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWorkForBHHCRepository _repository;

        public HomeController(ILogger<HomeController> logger, IWorkForBHHCRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Reason()
        {
            var results = _repository.GetAllReasons();
            return View();
        }
       
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Reason(ReasonViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _repository.PersistReasonAsync(model, User.Identity.Name);
                return RedirectToAction("Index", "Home");
            }
            {
                return BadRequest();
            }
        }

        [Authorize]
        public IActionResult ReasonList()
        {
            var results = _repository.GetAllReasonsByUser(User.Identity.Name);
            return View(results);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
