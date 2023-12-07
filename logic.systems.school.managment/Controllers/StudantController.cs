﻿using logic.systems.school.managment.Dto;
using logic.systems.school.managment.Interface;
using logic.systems.school.managment.Mapper.ManualMapper;
using logic.systems.school.managment.Models;
using logic.systems.school.managment.Services;
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
                return View(new CreateStudantDTO()
                {

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
                  
                    var result = await _StudentService.Create(StudantProfile.ToClass(model), "8e445865-a24d-4543-a6c6-9443d048cdb9");
                    await PopulateForms();
                    return RedirectToAction("Edit", result.Id);
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
                var model = await _StudentService.Read(id); 
                await PopulateForms();
                return View(StudantProfile.ToDTO(model));
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
