using logic.systems.school.managment.Interface;
using logic.systems.school.managment.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Metrics;

namespace logic.systems.school.managment.Controllers
{
    public class StudantController : Controller
    {
        private ICRUD<Student> _StudentService;
        private IOrgUnit _IOrgUnitServiceService;

        public StudantController(ICRUD<Student> StudentService, IOrgUnit IOrgUnitServiceService)
        {
            this._StudentService = StudentService;
            this._IOrgUnitServiceService = IOrgUnitServiceService;
        }

        public async Task<IActionResult> Index(int? pageNumber = 1, int? pageSize = 10)
        {
            try
            {
                return View(await _StudentService.ReadPagenation(pageNumber.Value, pageSize.Value));
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
                return View(new Student()
                {

                });
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
        }
    }
}
