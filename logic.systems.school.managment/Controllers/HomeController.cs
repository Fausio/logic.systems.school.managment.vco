using logic.systems.school.managment.Dto;
using logic.systems.school.managment.Interface;
using logic.systems.school.managment.Models;
using logic.systems.school.managment.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace logic.systems.school.managment.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ILogger<HomeController> _logger;
        private ITuitionService _ITuitionService;
        private IDashBoard _IDashBoard;
        private ISempleEntityService _SempleEntityService;
        public HomeController(ILogger<HomeController> logger,
             ITuitionService iTuitionService,
             UserManager<AppUser> userManager,
                ISempleEntityService SempleEntityService,
             IDashBoard iDashBoard)
        {
            this._logger = logger;
            this._ITuitionService = iTuitionService;
            this._IDashBoard = iDashBoard;
            this._SempleEntityService = SempleEntityService;
            this._userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            // update  multas
            ViewBag.CurrentSchoolLevels = await _SempleEntityService.GetByTypeOrderById("SchoolLevel");
 
            return View(new StudentPageDto()
            {
                studentTotals = await _IDashBoard.GetStudentTotals()
            }) ;
        }


        public async Task<IActionResult> getfeeBymonth()
        {
            var result = await _IDashBoard.GetAllMonthsFeeByCurrentYear();
            return Json(result);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}