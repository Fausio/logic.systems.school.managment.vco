using logic.systems.school.managment.Dto;
using logic.systems.school.managment.Interface;
using logic.systems.school.managment.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace logic.systems.school.managment.Controllers
{
    [Authorize]
    public class TuitionController : Controller
    {
        private ITuitionService _ITuitionService;
        private   UserManager<IdentityUser> _userManager;
        public TuitionController(ITuitionService ITuitionService, UserManager<IdentityUser> userManager)
        {
            this._ITuitionService = ITuitionService;
            this._userManager = userManager;
        }
        public async Task<JsonResult> IndexByStudantId(getPaymentParamitersDTO id)
        {
            var result = await _ITuitionService.GetByStudantId(id.StudantId);
            result = result.Where(x => x.Year  == id.EnrollmentYear).ToList();
            return Json(result);
        }
        public async Task<JsonResult> IndexByStudantIdFines(int id)
        {
            try
            {
                var result = await _ITuitionService.GetByStudantIdFinesBy(id);
                foreach (var item in result)
                {
                    item.Tuition.TuitionFines = null;
                }
                return Json(result);
            }
            catch (Exception e)
            {

                throw e;
            }

        }

        public async Task<JsonResult> IndexPaymentByStudantId(getPaymentParamitersDTO id)
        {
            var result = await _ITuitionService.GetPaymentsByStudantTuitionsId(id.StudantId);
            result = result.Where(x => x.Tuition.Year == id.EnrollmentYear).ToList();

            return Json(result);
        }

        public async Task<JsonResult> IndexMultiPaymentByStudantId(getPaymentParamitersDTO id)
        {
            //var result = await _ITuitionService.GetPaymentsByStudantTuitionsId(id.StudantId);
            //result = result.Where(x => x.Tuition.Year == id.EnrollmentYear).ToList(); 
            //return Json(result);
            var result = await _ITuitionService.GetByStudantId(id.StudantId);
            result = result.Where(x => x.Year == id.EnrollmentYear).ToList();

            var resultDTO = new List<MultiPaymentTuitionDTO>(); 
            foreach (var item in result)
            {
                var _schoolLevel = item.Enrollment.SchoolLevel.Description;
                var _tuitionValue = _ITuitionService.getTuitionValueByschoolLevel(_schoolLevel);
                resultDTO.Add(new MultiPaymentTuitionDTO()
                {
                    id = item.Id,
                    endDate = item.EndDate,
                    monthName = item.MonthName,
                    startDate = item.StartDate,
                    tuitionValue = _tuitionValue,
                });
            }

        
            return Json(resultDTO);
        }

        public async Task<JsonResult> IndexByEnrollments(int id)
        {
            var result = await _ITuitionService.GetPaymentsByStudantTuitionsId(id);
            return Json(result);
        }

        [HttpPost]
        public async Task<JsonResult> CreatePayment(CreatePaymentDTO data)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            var result = await _ITuitionService.CreatePayment(data, currentUser.Id);
            return Json("");
        }
        [HttpPost]

        public async Task<JsonResult> CreateFeePayment(CreateFeePaymentDTO data)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            await _ITuitionService.CreateFeePayment(data, currentUser.Id);
            return Json("");
        }
    }
}
