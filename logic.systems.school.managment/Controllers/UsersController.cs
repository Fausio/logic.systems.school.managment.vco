﻿using logic.systems.school.managment.Areas.Identity.Pages.Account;
using logic.systems.school.managment.Data;
using logic.systems.school.managment.Dto;
using logic.systems.school.managment.Interface;
using logic.systems.school.managment.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace logic.systems.school.managment.Controllers
{
    [Authorize(Roles = "ADMINISTRATOR")]
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly ISempleEntityService _SimpleEntityService;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _userRoleManager;
        private List<string> admins = new List<string> { "admin@pandaalegria.com" };

        public UsersController(ISempleEntityService simpleEntityService, UserManager<AppUser> userManager, RoleManager<IdentityRole> userRoleManager)
        {
            _userManager = userManager;
            _SimpleEntityService = simpleEntityService;
            db = new ApplicationDbContext(new DbContextOptions<ApplicationDbContext>());
            _userRoleManager = userRoleManager;
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Models.RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var AppUser = await _userManager.GetUserAsync(User);
                var now = DateTime.Now;

                var user = new AppUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,

                };
                var result = await _userManager.CreateAsync(user, model.Password);
                await _userManager.AddToRoleAsync(user, "EMPLOYEE".ToUpper());
                if (result.Succeeded)
                {
                    @TempData["success"] = "Utilizador criado com sucesso";
                    return RedirectToAction(nameof(Index));
                }
            }

            var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
            var error_mgs = string.Join("\n; ", errors);

            @TempData["error"] = error_mgs;
            return View(model);
        }

        public async Task<IActionResult> Create()
        {
            return View(new Models.RegisterModel());
        }

        public async Task<IActionResult> Index()
        {
            var users_ = await _userManager.Users.ToListAsync();
            List<AppUser> users = users_.Where(x => !admins.Contains(x.Email)).ToList();

            foreach (var item in users)
            {
                var role = await _userManager.GetRolesAsync(item);
                item.RoleName = role.FirstOrDefault();
            }

            return View(users);
        }

        public async Task<IActionResult> Update(string id)
        {

            if (id == null)
            {
                return NotFound();
            }

            AppUser user = (AppUser)await db.Users.FirstOrDefaultAsync(u => u.Id == id);


            if (user == null)
            {
                return NotFound();
            }

            ViewBag.Roles = await _userRoleManager.Roles.Where(x => x.Name != "administrator".ToUpper()).ToListAsync();
             
            var userInfo = new UpdateUserInfoDTO()
            {
                UserId = user.Id,
                Email = user.Email,
            };




            var passDto = new UpdatePasswordDTO()
            {
                UserId = user.Id,
                NewPassword = string.Empty
            };
            var model = new UpdateUserDTO()
            {
                UserInfo = userInfo,
                PasswordDTO = passDto
            };


            var role = await _userManager.GetRolesAsync(user);
            model.RoleName = role.FirstOrDefault();

            if (admins.Contains(userInfo.Email))
            {
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdatePassword(UpdateUserDTO model)
        {

            var AppUser = await _userManager.GetUserAsync(User);
            var now = DateTime.Now;

            var stringId = model.PasswordDTO.UserId.ToString();

            var user = await _userManager.FindByIdAsync(stringId);
            if (user == null)
            {
                TempData["error"] = "Usuário não encontrado.";
                return RedirectToAction("Update", new { id = model.PasswordDTO.UserId });
            }

            // Remove current password (if it exists)
            var removePasswordResult = await _userManager.RemovePasswordAsync(user);
            if (!removePasswordResult.Succeeded)
            {
                TempData["error"] = "Erro ao remover a senha atual.";
                return RedirectToAction("Update", new { id = model.PasswordDTO.UserId });
            }

            // Add new password
            var addPasswordResult = await _userManager.AddPasswordAsync(user, model.PasswordDTO.NewPassword);
            if (addPasswordResult.Succeeded)
            {
                TempData["success"] = "Senha atualizada com sucesso.";
                return RedirectToAction("Update", new { id = model.PasswordDTO.UserId });
            }

            @TempData["error"] = "Erro ao actualizado o  utilizador";
            return RedirectToAction("Update", new { id = model.PasswordDTO.UserId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> updateInfoUser(UpdateUserDTO model)
        {


            var AppUser = await _userManager.GetUserAsync(User);
            var now = DateTime.Now;

            var stringId = model.UserInfo.UserId.ToString();
            var user = await _userManager.FindByIdAsync(stringId);
            if (user == null)
            {
                TempData["error"] = "Usuário não encontrado.";
                return RedirectToAction("Update", new { id = model.UserInfo.UserId });
            }

            user.NormalizedEmail = model.UserInfo.Email;
            user.NormalizedUserName = model.UserInfo.Email;
            user.Email = model.UserInfo.Email;
            user.UserName = model.UserInfo.Email;


            await _userManager.UpdateAsync(user);

            TempData["success"] = "Dados atualizados com sucesso.";
            return RedirectToAction("Update", new { id = model.UserInfo.UserId });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateProfile(UpdateUserDTO model)
        {

            var AppUser = await _userManager.GetUserAsync(User);
            var now = DateTime.Now;

            var stringId = model.PasswordDTO.UserId.ToString();

            var user = await _userManager.FindByIdAsync(stringId);
            if (user == null)
            {
                TempData["error"] = "Usuário não encontrado.";
                return RedirectToAction("Update", new { id = model.PasswordDTO.UserId });
            }
            // remove roles
            var roles = await _userManager.GetRolesAsync(user); 
            foreach (var role in roles)
            {
                await _userManager.RemoveFromRoleAsync(user, role);
            } 
            // update profile
            var result = await _userManager.AddToRoleAsync(user, model.RoleName.ToUpper());
            if (result.Succeeded)
            {
                TempData["success"] = "Perfil atualizado com sucesso.";
                return RedirectToAction("Update", new { id = model.PasswordDTO.UserId });
            }

            @TempData["error"] = "Erro ao actualizado o  utilizador";
            return RedirectToAction("Update", new { id = model.PasswordDTO.UserId });
        }
    }
}
