using logic.systems.school.managment.Data;
using logic.systems.school.managment.Dto;
using Microsoft.AspNetCore.Mvc;

namespace logic.systems.school.managment.Controllers
{
    public class DocumentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult PaymentTuitionList()
        {
            return View(new ReportDataFilterDTO());
        }

        [HttpPost]
        public IActionResult PaymentTuitionList(ReportDataFilterDTO filters)
        {
            return View(new ReportDataFilterDTO());
        }


    }
}
