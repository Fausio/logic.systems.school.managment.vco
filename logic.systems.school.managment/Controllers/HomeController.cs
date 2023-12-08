using logic.systems.school.managment.Interface;
using logic.systems.school.managment.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace logic.systems.school.managment.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ITuitionService _ITuitionService;
        public HomeController(ILogger<HomeController> logger,
             ITuitionService iTuitionService)
        {
            _logger = logger;
            this._ITuitionService = iTuitionService;
        }

        public async Task<IActionResult> Index()
        {
            // update  multas
            await _ITuitionService.CheckFee();
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}