using Microsoft.AspNetCore.Mvc;

namespace logic.systems.school.managment.Controllers
{
    public class LogController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
