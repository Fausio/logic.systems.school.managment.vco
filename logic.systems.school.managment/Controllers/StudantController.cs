using logic.systems.school.managment.Data;
using logic.systems.school.managment.Dto;
using logic.systems.school.managment.Interface;
using logic.systems.school.managment.Mapper.ManualMapper;
using logic.systems.school.managment.Models;
using logic.systems.school.managment.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace logic.systems.school.managment.Controllers
{
    [Authorize]
    public class StudantController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
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
        UserManager<AppUser> userManager)
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
                var currentUser = await _userManager.GetUserAsync(User);

                await PopulateForms();
                return View(new CreateStudantDTO()
                {
                    EnroolAllMonths = true,
                    CreatedUSer = currentUser.Id
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

                    var currentUser = await _userManager.GetUserAsync(User);

                    // todo: ver duplicacação pelo BI 
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

                    if (await _StudentService.CheckIfExists(model.PersonId))
                    {
                        TempData["MensagemError"] = $"Erro de duplicação de dados: Impossível gravar o estudante {model.Name} porque já existe um estudante com o BI {model.PersonId}. na base de dados";
                        if (TempData.ContainsKey("MensagemError"))
                        {
                            ViewBag.MensagemError = TempData["MensagemError"];
                        }
                        return View(model);
                    }

                    var result = await _StudentService.Create(StudantProfile.ToClass(model), currentUser.Id);
                    var Enrollment = await _IEnrollmentService.EnrollmentByStudantId(result.Id, model.CurrentSchoolLevelId, model.EnrollmentYear, result.SchoolClassRoomId, model.EnrollmentPrice, model.TuitionPrice);
                    await _ITuitionService.CreateByClassOfStudant(result, Enrollment, currentUser.Id);
                    TempData["MensagemSucess"] = "Estudante Registrado com sucesso!";
                    return RedirectToAction("edit", "studant", new { id = result.Id });
                }

                return View(model);
            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task<IActionResult> Transfer(int Id)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            await _StudentService.Transfer(Id, currentUser.Id);
            TempData["MensagemSucess"] = "Tranferencia do estudante feita com sucesso!";
            return RedirectToAction("edit", "studant", new { id = Id });
        }
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var currentUser = await _userManager.GetUserAsync(User);
                await _ITuitionService.CheckFee(id, currentUser.Id);

                var model = await _StudentService.Read(id);
                var result = StudantProfile.ToDTO(model);

                await PopulateForms();
                await PopuLateDetailsForm(model);

                var currentSchoolLevels = await _SempleEntityService.GetByTypeOrderById("SchoolLevel");
                ViewBag.CurrentSchoolLevels = currentSchoolLevels.Where(x => x.Id > result.CurrentSchoolLevelId);
                ViewBag.FixCurrentSchoolLevels = currentSchoolLevels;
                ViewBag.EnrollmentYears = new List<string>{
                   DateTime.Now.AddYears(1).Year.ToString(),
                 };

                ViewBag.FixEnrollmentYears = new List<string>{
                    DateTime.Now.Year.ToString(),
                     DateTime.Now.AddYears(1).Year.ToString(),
                 };

                var ClassRoom = await _SempleEntityService.GetById(result.SchoolClassRoomId);

                if (model.BirthDate != null)
                {
                    result.age = model.GetAgeInDay();
                }

                var createdUser = await _userManager.FindByIdAsync(model.CreatedUSer);

                if (createdUser is not null)
                {
                    result.CreatedUSer = createdUser.UserName;
                    result.CreatedDate = model.CreatedDate;
                }

                if (model.UpdatedDate is not null)
                {
                    var updatedUser = await _userManager.FindByIdAsync(model.UpdatedUSer);

                    if (updatedUser is not null)
                    {
                        result.UpdatedUSer = updatedUser.UserName;
                        result.UpdatedDate = model.UpdatedDate.Value;
                    }
                }

                ViewBag.SchoolClassRoom = ClassRoom.Description;

                if (TempData.ContainsKey("MensagemSucess"))
                {
                    ViewBag.Mensagem = TempData["MensagemSucess"];
                }



                var products = await _StudentService.ReadProducts();
                var SalesProducts = new SalesProductDTO();


                SalesProducts.Products = products.Select(x => new ProductDropDownDTO()
                {
                    Id = x.Id,
                    Description = x.Description,
                    Price = x.Price,

                }).ToList();

                result.SalesProduct = SalesProducts;
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
                await _IEnrollmentService.UpdatePrices(model);
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

                var currentEnrolment = model.Enrollments.LastOrDefault();


                if (currentEnrolment is not null)
                {
                    result.EnrollmentYear = currentEnrolment.EnrollmentYear;
                    result.EnrollmentPrice = currentEnrolment.EnrollmentPrice;
                    result.TuitionPrice = currentEnrolment.TuitionPrice;
                }

                await PopulateForms();

                var createdUser = await _userManager.FindByIdAsync(model.CreatedUSer);

                if (createdUser is not null)
                {
                    result.CreatedUSer = createdUser.UserName;
                    result.CreatedDate = model.CreatedDate;
                }

                if (model.UpdatedDate is not null)
                {
                    var updatedUser = await _userManager.FindByIdAsync(model.UpdatedUSer);

                    if (updatedUser is not null)
                    {
                        result.UpdatedUSer = updatedUser.UserName;
                        result.UpdatedDate = model.UpdatedDate.Value;
                    }
                }


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
               Student.genderM,
               Student.genderF
            };

            ViewBag.DiscountType = new List<string>{
               Student.DiscountWithout,
               Student.DiscountPersonInCharge,
               Student.DiscountTeacher,
            };

            ViewBag.Provinces = await _IOrgUnitServiceService.GetOrgUnitProvinces();
            ViewBag.District = await _IOrgUnitServiceService.GetOrgUnitDistricts();
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


        public async Task<IActionResult> delete(DeleteStudentDTO dto)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            await _StudentService.Delete(dto, currentUser.Id);
            return RedirectToAction("index");
        }
    }
}
