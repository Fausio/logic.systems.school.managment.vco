using logic.systems.school.managment.Dto;
using logic.systems.school.managment.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace logic.systems.school.managment.Controllers
{
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
                 
                return View(new SalesProductDTO()
                {
                    Student =  await _StudentService.Read(id),
                    Products = await _StudentService.ReadProducts()
                });
            }
            catch (Exception)
            {

                throw;
            }

        }


    }
}
