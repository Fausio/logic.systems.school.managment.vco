using logic.systems.school.managment.Interface;
using Microsoft.AspNetCore.Mvc;

namespace logic.systems.school.managment.Controllers
{
    public class TuitionController : Controller
    {
        private ITuitionService _ITuitionService;

        public TuitionController(ITuitionService  ITuitionService )
        {
                this._ITuitionService = ITuitionService;
        }
        public async Task<JsonResult> IndexByStudantId(int id)
        {
            var result = await _ITuitionService.GetByStudantId(id);
            return Json(result);
        }
    }
}
