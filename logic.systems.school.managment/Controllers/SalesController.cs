﻿using logic.systems.school.managment.Dto;
using logic.systems.school.managment.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace logic.systems.school.managment.Controllers
{
    [Authorize]
    public class SalesController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private IstudantService _StudentService; 
        private ISalesService _SalesService;    

        public SalesController(IstudantService StudentService,
            ISalesService SalesService, 
        UserManager<IdentityUser> userManager)
        {
            this._StudentService = StudentService; 
            this._userManager = userManager;
            this._SalesService = SalesService;
        }


        public async Task<IActionResult> Create(int id)
        {
            try
            {
                var currentUser = await _userManager.GetUserAsync(User);
                var products = await _StudentService.ReadProducts();
                 
                return View(new SalesProductDTO()
                {
                    Student =  await _StudentService.Read(id),
                    Products = products.Select(x => new ProductDropDownDTO()
                    {
                        Id = x.Id,
                        Description = x.Description,
                        Price = x.Price,

                    }).ToList(),
                });
            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task<IActionResult> _Create(int id)
        {
            try
            {
                var currentUser = await _userManager.GetUserAsync(User);
                var products = await _StudentService.ReadProducts();

                return View(new SalesProductDTO()
                {
                    Student = await _StudentService.Read(id),
                    Products = products.Select(x => new ProductDropDownDTO()
                    {
                        Id = x.Id,
                        Description = x.Description,
                        Price = x.Price,

                    }).ToList(),
                });
            }
            catch (Exception)
            {

                throw;
            }

        }

        [HttpPost]
        public async Task<JsonResult> Sell([FromBody] List<ProductSellDTO> obj)
        {
            try
            {
              

              var currentUser = await _userManager.GetUserAsync(User);
               await _SalesService.Sell(obj, currentUser.Id);
               return Json("");
            }
            catch (Exception)
            {

                throw;
            }

        }

         
    }
}
