using logic.systems.school.managment.Dto;
using logic.systems.school.managment.Interface;
using logic.systems.school.managment.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace logic.systems.school.managment.Controllers
{
    public class SettingController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private IOrgUnit _IOrgUnitServiceService;
        private ITuitionService _ITuitionService;
        private IEnrollment _IEnrollmentService;
        public SettingController(IOrgUnit IOrgUnitServiceService, UserManager<IdentityUser> userManager, ITuitionService iTuitionService, IEnrollment iEnrollmentService)
        {
            this._IOrgUnitServiceService = IOrgUnitServiceService;
            this._userManager = userManager;
            _ITuitionService = iTuitionService;
            _IEnrollmentService = iEnrollmentService;
        }


        public async Task<IActionResult> CreateDistrict()
        {
            await PopulateForm();
            return View(new OrgUnitDistrictCreateDTO());
        }

        [HttpPost]
        public async Task<IActionResult> CreateDistrict(OrgUnitDistrictCreateDTO dto)
        {
            await PopulateForm();

            if (ModelState.IsValid)
            {
                if (await _IOrgUnitServiceService.CkeckIfCreateOrgUnitDistrictsExists(dto))
                {
                    TempData["MensagemError"] = $"Impossível gravar o districto {dto.Description} pois já existe na província escolhida.";
                    if (TempData.ContainsKey("MensagemError"))
                    {
                        ViewBag.MensagemError = TempData["MensagemError"];
                    }
                    return View(dto);
                }
                else
                {
                    var currentUser = await _userManager.GetUserAsync(User);
                    await _IOrgUnitServiceService.CreateOrgUnitDistricts(dto, currentUser.Id);
                    return View(dto);
                }
            }

            return View(new OrgUnitDistrictCreateDTO());
        }


        public async Task<JsonResult> GetDistricts(int Id)
        {
            var resultFromDb = await _IOrgUnitServiceService.GetOrgUnitDistrictsByProvinceId(Id);

            resultFromDb = resultFromDb.OrderBy(x => x.Description).ToList();


            var result = new List<string>();

            resultFromDb.ForEach((x) =>
            {
                result.Add(x.Description);
            });

            return Json(result);
        }

        public IActionResult GetAllOrgunits()
        {
            return View(new OrgUnitDistrictCreateDTO());
        }

        private async Task PopulateForm()
        {
            ViewBag.Provinces = await _IOrgUnitServiceService.GetOrgUnitProvinces();
        }


        public async Task<IActionResult> tuitionConfiguration()
        {
            var result = await _ITuitionService.ReadYearDefinitions();
            return View(result);
        }
        public async Task<IActionResult> EnrollmentConfiguration()
        { 
            var result = await _IEnrollmentService.ReadYearDefinitions();
            return View(result);
        }



        public async Task<IActionResult> EditEnrollmentPrice(int id)
        {
            return View(await _IEnrollmentService.ReadEnrolmentPriceById(id));
        }
        public async Task<IActionResult> EditTuitionPrice(int id)
        {
            return View(await _ITuitionService.ReadTuitionPriceById(id));
        }


        [HttpPost]
        public async Task<IActionResult> EditEnrollmentPrice(EnrollmentPrice entity)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            entity.UpdatedUSer = currentUser.Email;
            ViewBag.Mensagem = "Matricula actualizada com sucesso!";
            return View(await _IEnrollmentService.UpdateEnrollmentPrice(entity));
        }

        [HttpPost]
        public async Task<IActionResult> EditTuitionPrice(TuitionPrice entity)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            entity.UpdatedUSer = currentUser.Email;
            ViewBag.Mensagem = "Propina actualizada com sucesso!";
            return View(await _ITuitionService.UpdateTuitionPrice(entity));
        }


        public async Task<IActionResult> generateTuitionPrice(int id)
        {
            await _ITuitionService.generateTuitionPrice(id);
            ViewBag.Mensagem = "Propina geradas com sucesso!";
            return RedirectToAction("tuitionConfiguration");
        }    
        public async Task<IActionResult> generateEnrollmentPrice(int id)
        {
            await _IEnrollmentService.generateEnrolmentPrice(id);
            ViewBag.Mensagem = "Matriculas geradas com sucesso!";
            return RedirectToAction("EnrollmentConfiguration");
        }

    }
}
