using logic.systems.school.managment.Dto;
using logic.systems.school.managment.Interface;
using logic.systems.school.managment.Models;
using Microsoft.AspNetCore.Mvc;

namespace logic.systems.school.managment.Controllers
{
    public class TuitionController : Controller
    {
        private ITuitionService _ITuitionService;

        public TuitionController(ITuitionService ITuitionService)
        {
            this._ITuitionService = ITuitionService;
        }
        public async Task<JsonResult> IndexByStudantId(int id)
        {
            var result = await _ITuitionService.GetByStudantId(id);
            return Json(result);
        }
         public async Task<JsonResult> IndexByStudantIdFines(int id)
        {
            var result = await _ITuitionService.GetByStudantIdFinesBy(id);
            return Json(result);
        }

        public async Task<JsonResult> IndexPaymentByStudantId(int id)
        {
            var result = await _ITuitionService.GetPaymentsByStudantTuitionsId(id);
            return Json(result);
        }

        [HttpPost]
        public async Task<JsonResult> CreatePayment(CreatePaymentDTO data)
        {
            var result = await _ITuitionService.CreatePayment(data);
            return Json(result);
        }
        [HttpPost]

        public async Task<JsonResult> CreateFeePayment(CreateFeePaymentDTO data)
        {
            await _ITuitionService.CreateFeePayment(data);
            return Json("");
        }
    }
}
