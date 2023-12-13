using logic.systems.school.managment.Interface;
using logic.systems.school.managment.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace logic.systems.school.managment.Controllers
{
    public class AdminController : Controller
    {
        private IOrgUnit _IOrgUnitServiceService;
        private ISempleEntityService _SempleEntityService;
        public AdminController(IOrgUnit IOrgUnitServiceService, ISempleEntityService sempleEntityService)
        {
            this._IOrgUnitServiceService = IOrgUnitServiceService;
            _SempleEntityService = sempleEntityService;
        }

        public async Task<JsonResult> GetDistricts(int Id)
        {
            var s = await _IOrgUnitServiceService.GetOrgUnitDistrictsByProvinceId(Id);
            return Json(await _IOrgUnitServiceService.GetOrgUnitDistrictsByProvinceId(Id));
        }
        public async Task<JsonResult> GetSchoolClassRooms(int Id)
        {
            var s = await _SempleEntityService.GetGetSchoolClassRoomsBySchoolLevelId(Id);
            return Json(await _IOrgUnitServiceService.GetOrgUnitDistrictsByProvinceId(Id));
        }
    }
}
