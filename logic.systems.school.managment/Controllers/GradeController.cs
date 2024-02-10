using logic.systems.school.managment.Dto;
using logic.systems.school.managment.Interface;
using logic.systems.school.managment.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace logic.systems.school.managment.Controllers
{
    public class GradeController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private ISempleEntityService _SempleEntityService;

        public GradeController(UserManager<IdentityUser> _userManager, ISempleEntityService SempleEntityService)
        {
            this._userManager = _userManager;
            this._SempleEntityService = SempleEntityService;
        }
        public async Task<IActionResult> Index()
        {
            await ConfigView();
            return View(new GradeConfigDTO());
        }

        public async Task<IActionResult> Config(GradeConfigDTO dto)
        {
            await ConfigView();

            if (ModelState.IsValid)
            {
                return RedirectToAction("index");
            }


            return View(dto);
        }



        private async Task ConfigView()
        {

            ViewBag.SchoolLevels = await _SempleEntityService.GetByTypeOrderById("SchoolLevel");
            ViewBag.SchoolClassRooms = await _SempleEntityService.GetByTypeOrderById("SchoolClassRoom");
            ViewBag.Subjects = await _SempleEntityService.GetByTypeOrderById("Subject");
            ViewBag.Quarters = new List<int>() { 1, 2, 3 };
               
            

        }
    }
}
