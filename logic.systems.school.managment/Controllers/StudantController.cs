﻿using logic.systems.school.managment.Dto;
using logic.systems.school.managment.Interface;
using logic.systems.school.managment.Mapper.ManualMapper;
using logic.systems.school.managment.Models;
using logic.systems.school.managment.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Metrics;

namespace logic.systems.school.managment.Controllers
{
    [Authorize]
    public class StudantController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private IstudantService _StudentService;
        private IOrgUnit _IOrgUnitServiceService;
        private ISempleEntityService _SempleEntityService;
        private ITuitionService _ITuitionService;
        private IEnrollment _IEnrollmentService;
        private IApp _IAppService;

        public StudantController(IstudantService StudentService,
            IOrgUnit IOrgUnitServiceService,
            ISempleEntityService SempleEntityService,
            IEnrollment IEnrollment,
            IApp IAppService,
        ITuitionService iTuitionService,
        UserManager<IdentityUser> userManager)
        {
            this._StudentService = StudentService;
            this._IOrgUnitServiceService = IOrgUnitServiceService;
            this._SempleEntityService = SempleEntityService;
            this._ITuitionService = iTuitionService;
            this._IEnrollmentService = IEnrollment;
            this._IAppService = IAppService;
            this._userManager = userManager;
        }

        public async Task<IActionResult> Index(int? pageNumber = 1, int? pageSize = 10)
        {
            try
            {
                var result = await _StudentService.ReadPagenation(pageNumber.Value, pageSize.Value);
                ViewBag.CurrentSchoolLevels = await _SempleEntityService.GetByTypeOrderById("SchoolLevel");
                return View(new StudentPageDto()
                {
                    indexPage = result
                });
            }
            catch (Exception)
            { 
                throw;
            }

        }

        public async Task<IActionResult> search(StudentPageDto model)
        {
            try
            {

                if (model.CurrentSchoolLevelId == null)
                {
                    model.CurrentSchoolLevelId = 0;
                }

                var result = await _StudentService.SearchRecord(model.studentName, model.CurrentSchoolLevelId.Value);
                ViewBag.CurrentSchoolLevels = await _SempleEntityService.GetByTypeOrderById("SchoolLevel");
                return View("Index", new StudentPageDto()
                {
                    indexPage = result
                });
            }
            catch (Exception)
            {

                throw;
            }

        }



        public async Task<IActionResult> Create()
        {
            try
            {
                await PopulateForms();
                return View(new CreateStudantDTO()
                {
                    EnroolAllMonths = true,
                });
            }
            catch (Exception)
            {

                throw;
            }

        }

        [HttpPost]
        public async Task<IActionResult> Create(Dto.CreateStudantDTO model)
        {
            try
            {
                await PopulateForms();




                if (ModelState.IsValid)
                {
                    if (await _IAppService.LimitOfStudentByClassRoomAndLevelYear(model.EnrollmentYear, model.CurrentSchoolLevelId, model.SchoolClassRoomId))
                    {

                        var CurrentSchoolLevel = await _IAppService.SempleEntityDescriptionById(model.CurrentSchoolLevelId);
                        var SchoolClassRoom = await _IAppService.SempleEntityDescriptionById(model.SchoolClassRoomId);
                        TempData["MensagemError"] = $"Impossível gravar o estudante nesta turma. A turma {SchoolClassRoom} para {CurrentSchoolLevel} já atingiu o limite de 35 alunos para o ano {model.EnrollmentYear}.";
                        if (TempData.ContainsKey("MensagemError"))
                        {
                            ViewBag.MensagemError = TempData["MensagemError"];
                        }
                        return View(model);
                    }

                    //if (model.EnroolAllMonths)
                    //{
                    var currentUser = await _userManager.GetUserAsync(User);
                    var result = await _StudentService.Create(StudantProfile.ToClass(model), currentUser.Id);
                    var Enrollment = await _IEnrollmentService.EnrollmentByStudantId(result.Id, model.CurrentSchoolLevelId, model.EnrollmentYear, result.SchoolClassRoomId);
                    await _ITuitionService.CreateByClassOfStudant(result, Enrollment);
                    TempData["MensagemSucess"] = "Estudante Registrado com sucesso!";
                    return RedirectToAction("edit", "studant", new { id = result.Id });
                    //}
                    //else if (!model.EnroolAllMonths && model.StartTuition > 0)
                    //{
                    //    var result = await _StudentService.Create(StudantProfile.ToClass(model), "8e445865-a24d-4543-a6c6-9443d048cdb9");
                    //    await _ITuitionService.CreateByClassOfStudant(result, model.StartTuition);
                    //    TempData["MensagemSucess"] = "Estudante Registrado com sucesso!";
                    //    return RedirectToAction("edit", "studant", new { id = result.Id });
                    //}
                    //else
                    //{

                    //    TempData["MensagemError"] = "Escolha o Meses de início";
                    //    if (TempData.ContainsKey("MensagemError"))
                    //    {
                    //        ViewBag.MensagemError = TempData["MensagemError"];
                    //    }
                    //    return View(model);
                    //}


                }

                return View(model);
            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                await _ITuitionService.CheckFee(id);
                await _ITuitionService.AutomaticRegularization(id);
                var model = await _StudentService.Read(id);
                var result = StudantProfile.ToDTO(model);
                await PopulateForms();
                await PopuLateDetailsForm(model);

                var currentSchoolLevels = await _SempleEntityService.GetByTypeOrderById("SchoolLevel");
                ViewBag.CurrentSchoolLevels = currentSchoolLevels.Where(x => x.Id > result.CurrentSchoolLevelId);
                ViewBag.EnrollmentYears = new List<string>{
                   DateTime.Now.AddYears(1). Year.ToString(),
            };

                if (TempData.ContainsKey("MensagemSucess"))
                {
                    ViewBag.Mensagem = TempData["MensagemSucess"];
                }
                return View(result);
            }
            catch (Exception)
            {

                throw;
            }

        }


        [HttpPost]
        public async Task<IActionResult> Edit(EditStudantDTO model)
        {
            try
            {
                var myClass = StudantProfile.ToClass(model);
                myClass.Id = model.id;
                var currentUser = await _userManager.GetUserAsync(User);
                var result = await _StudentService.Update(myClass, currentUser.Id);

                TempData["MensagemSucess"] = "Estudante Actualizado com sucesso!";


                return RedirectToAction("EditStudant", "studant", new { id = model.id });
            }
            catch (Exception)
            {

                throw;
            }

        }


        public async Task<IActionResult> EditStudant(int id)
        {
            try
            {
                var model = await _StudentService.Read(id);
                var result = StudantProfile.ToDTO(model);
                await PopulateForms();
                if (TempData.ContainsKey("MensagemSucess"))
                {
                    ViewBag.Mensagem = TempData["MensagemSucess"];
                }
                return View(result);
            }
            catch (Exception)
            {

                throw;
            }

        }

        private async Task PopulateForms()
        {
            ViewBag.Gender = new List<string>{
                "Masculino",
                "Feminino"
            };

            ViewBag.Provinces = await _IOrgUnitServiceService.GetOrgUnitProvinces();
            ViewBag.CurrentSchoolLevels = await _SempleEntityService.GetByTypeOrderById("SchoolLevel");
            ViewBag.SchoolClassRooms = await _SempleEntityService.GetByTypeOrderById("SchoolClassRoom");
            ViewBag.EnrollmentYears = new List<string>{
                   DateTime.Now.Year.ToString(),
                   DateTime.Now.AddYears(1). Year.ToString(),
            };


        }

        private async Task PopuLateDetailsForm(Student Student)
        {
            ViewBag.Province = Student.District.OrgUnitProvince.Description;
            ViewBag.District = Student.District.Description;
            ViewBag.CurrentSchoolLevel = Student.CurrentSchoolLevel.Description;
            ViewBag.Tuitions = Student.Enrollments.SelectMany(x => x.Tuitions.Where(x => !x.Paid));
            ViewBag.TuitionsFee = await _ITuitionService.GetByStudantIdFinesBy(Student.Id);
        }
    }
}