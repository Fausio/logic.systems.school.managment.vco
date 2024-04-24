using logic.systems.school.managment.Dto;
using logic.systems.school.managment.Interface;
using logic.systems.school.managment.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace logic.systems.school.managment.Controllers
{
    [Authorize(Roles = "ADMINISTRATOR")]
    public class SettingController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private IOrgUnit _IOrgUnitServiceService;

        public SettingController(IOrgUnit IOrgUnitServiceService, UserManager<IdentityUser> userManager)
        {
            this._IOrgUnitServiceService = IOrgUnitServiceService;
            this._userManager = userManager;
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

        private async Task PopulateForm ()
        {
            ViewBag.Provinces = await _IOrgUnitServiceService.GetOrgUnitProvinces();
        }
    }
}
