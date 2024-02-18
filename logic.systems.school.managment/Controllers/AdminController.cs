using logic.systems.school.managment.Dto;
using logic.systems.school.managment.Interface;
using logic.systems.school.managment.Models;
using logic.systems.school.managment.Services;
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
        private ISalesService _SalesService;
        public AdminController(ISalesService SalesService, IOrgUnit IOrgUnitServiceService, ISempleEntityService sempleEntityService, ITuitionService iTuitionService, IEnrollment enrollmentService)
        {
            this._IOrgUnitServiceService = IOrgUnitServiceService;
            _SempleEntityService = sempleEntityService;
            _ITuitionService = iTuitionService;
            _EnrollmentService = enrollmentService;
            this._SalesService = SalesService;
        }

        public async Task<JsonResult> GetDistricts(int Id)
        {
            var result = await _IOrgUnitServiceService.GetOrgUnitDistrictsByProvinceId(Id);
            return Json(result);
        }
   
        public async Task<JsonResult> GetSubjects(int Id)
        {
            var result = await _SempleEntityService.GetSubjectsBySchoolLevel(Id);
            return Json(result);
        }

       public async Task<JsonResult> GetEnrollmentsByStudentId(int Id)
        {
            var result = await _EnrollmentService.EnrollmentsByStudantId(Id);
            return Json(result);
        }


    public async Task<JsonResult> GetgetProducts(int Id)
        {
            var result = await _SalesService.GetgetProducts(Id);
            return Json(result.Select(x => new ProductListTableDTO()
            {
                id = x.Id,
                date = x.Date.ToString("dd/MM/yyyy HH:mm:ss"),
                description = x.GetProducts(),
                paymentWithVat = x.GetProductsPrice().ToString("N2")
            }));
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
