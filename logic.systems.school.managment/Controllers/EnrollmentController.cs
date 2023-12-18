using DocumentFormat.OpenXml.Office2010.Excel;
using logic.systems.school.managment.Dto;
using logic.systems.school.managment.Interface;
using logic.systems.school.managment.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace logic.systems.school.managment.Controllers
{
    [Authorize]
    public class EnrollmentController : Controller
    {
        private IEnrollment _EnrollmentService;

        public EnrollmentController(IEnrollment enrollmentService)
        {
            _EnrollmentService = enrollmentService;
        }
        public async Task<IActionResult> Create(EnrollmentCreateDTO model)
        {
            if (!await _EnrollmentService.CheckIfHaveEnrollmentIntheYear(model))
            {
                var result = await _EnrollmentService.EnrollmentsByStudantId(model);
                return Json(result);
            }
            else
            {
                return Json("CheckIfHaveEnrollmentIntheYear");
            }

        }
        public async Task<IActionResult> CreateRepit(EnrollmentCreateDTO model)
        {
            if (!await _EnrollmentService.CheckIfHaveEnrollmentIntheYear(model))
            {
                var result = await _EnrollmentService.EnrollmentsByStudantId(model);
                return Json(result);
            }
            else
            {
                return Json("CheckIfHaveEnrollmentIntheYear");
            }

        }
   
    }
}
