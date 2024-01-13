using DocumentFormat.OpenXml.Office2010.Excel;
using logic.systems.school.managment.Dto;
using logic.systems.school.managment.Interface;
using logic.systems.school.managment.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace logic.systems.school.managment.Controllers
{
    [Authorize]
    public class EnrollmentController : Controller
    {
        private IEnrollment _EnrollmentService;
        private readonly UserManager<IdentityUser> _userManager;

        public EnrollmentController(IEnrollment enrollmentService, UserManager<IdentityUser> userManager)
        {
            _EnrollmentService = enrollmentService;
            this._userManager = userManager;
        }
        public async Task<IActionResult> Create(EnrollmentCreateDTO model)
        {
            if (!await _EnrollmentService.CheckIfHaveEnrollmentIntheYear(model))
            {
                var currentUser = await _userManager.GetUserAsync(User);
                var result = await _EnrollmentService.EnrollmentsByStudantId(model, currentUser.Id);
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
                var currentUser = await _userManager.GetUserAsync(User);
                var result = await _EnrollmentService.EnrollmentsByStudantId(model, currentUser.Id);
                return Json(result);
            }
            else
            {
                return Json("CheckIfHaveEnrollmentIntheYear");
            }

        }
   
    }
}
