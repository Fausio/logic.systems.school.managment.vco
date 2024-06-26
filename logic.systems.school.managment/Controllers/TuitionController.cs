﻿using logic.systems.school.managment.Dto;
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
        private IstudantService _StudentService;
        private ITuitionService _ITuitionService;
        private UserManager<IdentityUser> _userManager;
        public TuitionController(IstudantService StudentService,ITuitionService ITuitionService, UserManager<IdentityUser> userManager)
        {
            this._StudentService = StudentService;
            this._ITuitionService = ITuitionService;
            this._userManager = userManager;
        }
        public async Task<JsonResult> IndexByStudantId(getPaymentParamitersDTO id)
        {
            try
            {
                var result = await _ITuitionService.GetByStudantId(id.StudantId);

                result.ForEach(x => { x.Enrollment = null; });

                result = result.Where(x => x.Year == id.EnrollmentYear).ToList();
                return Json(result);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public async Task<JsonResult> IndexByStudantIdFines(int id)
        {
            try
            {
                var result = await _ITuitionService.GetByStudantIdFinesBy(id);
                foreach (var item in result)
                {
                    item.Tuition.TuitionFines = null;
                    item.Tuition.Enrollment = null;
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
            result = result.Where(x => x.TuitionYear == id.EnrollmentYear).ToList();

            return Json(result);
        }

        public async Task<JsonResult> IndexMultiPaymentByStudantId(getPaymentParamitersDTO id)
        {
            try
            {
                var result = await _ITuitionService.GetByStudantId(id.StudantId);
                result = result.Where(x => x.Year == id.EnrollmentYear && !x.Paid).ToList();

                var student = await _StudentService.Read(id.StudantId);
                var discount = (decimal)0;
                if (student is not null)
                {

                    if (student.DiscountType == Student.DiscountPersonInCharge)
                    {
                        discount = 100;
                    }
                    else if (student.DiscountType == Student.DiscountTeacher)
                    {
                        discount = 500;
                    }
                }

                var resultDTO = new List<MultiPaymentTuitionDTO>();
                foreach (var item in result)
                {
                    var _schoolLevel = item.Enrollment.SchoolLevel.Description;
                    var _tuitionValue = item.Enrollment.TuitionPrice;
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
            catch (Exception ex)
            {

                throw;
            }

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
            var dtoList = new List<CreatePaymentDTO>();
            dtoList.Add(data);
            await _ITuitionService.CreatePayment(dtoList, currentUser.Id);
            return Json("");
        }

        [HttpPost]
        public async Task<JsonResult> CreateMultiPayment([FromBody] List<CreatePaymentDTO> data)
        {
            var currentUser = await _userManager.GetUserAsync(User);

            await _ITuitionService.CreatePayment(data, currentUser.Id);
             
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
