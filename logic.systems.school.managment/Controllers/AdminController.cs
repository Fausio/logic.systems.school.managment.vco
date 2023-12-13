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
            var result = await _IOrgUnitServiceService.GetOrgUnitDistrictsByProvinceId(Id);
            return Json(result);
        }
        public async Task<JsonResult> GetSchoolClassRooms(int Id)
        {
            if (Id> 0)
            {
                var result = await _SempleEntityService.GetGetSchoolClassRoomsBySchoolLevelId(Id);
                return Json(result);
            }
            else
            {              
                return Json( new List<OrgUnitDistrict>());
            }
         
        }
    }
}
