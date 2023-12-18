using logic.systems.school.managment.Interface;
using logic.systems.school.managment.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace logic.systems.school.managment.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private IOrgUnit _IOrgUnitServiceService;
        private ISempleEntityService _SempleEntityService;
        private ITuitionService _ITuitionService;
        private IEnrollment _EnrollmentService;
        public AdminController(IOrgUnit IOrgUnitServiceService, ISempleEntityService sempleEntityService, ITuitionService iTuitionService, IEnrollment enrollmentService)
        {
            this._IOrgUnitServiceService = IOrgUnitServiceService;
            _SempleEntityService = sempleEntityService;
            _ITuitionService = iTuitionService;
            _EnrollmentService = enrollmentService;
        }

        public async Task<JsonResult> GetDistricts(int Id)
        {
            var result = await _IOrgUnitServiceService.GetOrgUnitDistrictsByProvinceId(Id);
            return Json(result);
        }

       public async Task<JsonResult> GetEnrollmentsByStudentId(int Id)
        {
            var result = await _EnrollmentService.EnrollmentsByStudantId(Id);
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
