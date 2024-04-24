using DocumentFormat.OpenXml.Office2010.Excel;
using logic.systems.school.managment.Dto;
using logic.systems.school.managment.Interface;
using logic.systems.school.managment.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace logic.systems.school.managment.Controllers
{
    [Authorize(Roles = "ADMINISTRATOR")]
    public class EnrollmentController : Controller
    {
        private IEnrollment _EnrollmentService;
        private IstudantService _StudentService;
        private ITuitionService _ITuitionService;
        private readonly UserManager<AppUser> _userManager;

        public EnrollmentController(IstudantService StudentService, IEnrollment enrollmentService, UserManager<AppUser> userManager, ITuitionService iTuitionService)
        {
            _EnrollmentService = enrollmentService;
            this._ITuitionService = iTuitionService;
            this._userManager = userManager;
            this._StudentService = StudentService;
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

        public async Task<IActionResult> Fix(FixEnrollmentDTO dto)
        {

            try
            {
                var currentUser = await _userManager.GetUserAsync(User);
                // todo: apagar aquela matricula, criar outra kkkkk
                await _EnrollmentService.DeleteParmanentyById(int.Parse(dto.OldEnrollment));

                // tood: update student 
                await _StudentService.UpdateEnrollment(dto, currentUser.Id);
           
                var Enrollment = await _EnrollmentService.EnrollmentByStudantId(dto.StudantId, int.Parse(dto.NewSchoolLevelId), int.Parse(dto.NewEnrollmentYear), int.Parse(dto.NewSchoolClassRoomId), dto.EnrollmentPrice, dto.TuitionPrice);
                await _ITuitionService.CreateByClassOfStudant(await _StudentService.Read(dto.StudantId), Enrollment, currentUser.Id);


                return Json("CheckIfHaveEnrollmentIntheYear");
            }
            catch (Exception ex)
            {

                throw ex;
            }
         

        }

    }
}
