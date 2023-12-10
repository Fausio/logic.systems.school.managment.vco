using logic.systems.school.managment.Interface;
using logic.systems.school.managment.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace logic.systems.school.managment.Controllers
{
    public class AdminController : Controller
    {
        private IOrgUnit _IOrgUnitServiceService;
        public AdminController(IOrgUnit IOrgUnitServiceService)
        {
            this._IOrgUnitServiceService = IOrgUnitServiceService;
        }

        public async Task<JsonResult> GetDistricts(int Id)
        {
            var s = await _IOrgUnitServiceService.GetOrgUnitDistrictsByProvinceId(Id);
            return Json(await _IOrgUnitServiceService.GetOrgUnitDistrictsByProvinceId(Id));
        }
    }
}
