using logic.systems.school.managment.Data;
using logic.systems.school.managment.Dto;
using logic.systems.school.managment.Interface;
using logic.systems.school.managment.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace logic.systems.school.managment.Services
{
    public class UserSirvice : IUserSirvice
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _userRoleManager;
        private readonly ApplicationDbContext db = new ApplicationDbContext(new DbContextOptions<ApplicationDbContext>());

        public UserSirvice(UserManager<AppUser> userManager, RoleManager<IdentityRole> userRoleManager)
        {
            _userManager = userManager;
            _userRoleManager = userRoleManager;
        }

        public Task<PaginationDTO<AppUser>> SearchRecord(string searchString)
        {
            throw new NotImplementedException();
        }
        public async Task<PaginationDTO<AppUser>> ReadPagenation(int pageNumber = 1, int pageSize = 10)
        {
            pageNumber = (pageNumber <= 0) ? 1 : pageNumber;
            pageSize = (pageSize <= 0) ? 10 : pageSize;


            var totalRecords = await db.Users.CountAsync();
            var totalPages = Math.Ceiling((double)totalRecords / pageSize);


            var skip = (pageNumber - 1) * pageSize;


            var models = new List<AppUser>();


            models = await db.Users
                                         .Skip(skip)
                                         .Take(pageSize)
                                         .Select(u => new AppUser()
                                         {
                                             Id = u.Id,
                                             Email = u.Email,
                                             UserName = u.UserName

                                         }).OrderBy(o => o.UserName).ToListAsync();



            var records = new PaginationDTO<AppUser>()
            {
                pageNumber = pageNumber,
                pageSize = pageSize,
                totalRecords = totalRecords,
                totalPages = totalPages,
                records = models,

            };


            return records;
        }

        public async Task GenerateStudentAccount(int studentId)
        {

            var studanteRole = "Estudante".ToUpper();
            var now = DateTime.Now;
            var student = await db.Students.FirstOrDefaultAsync(x => x.Id == studentId);
            var domainName = "logicSystems.co.mz";
            var passWord = new Random().Next(1000, 10000);
            var account = $"{student.Name}.{student.FatherName}@{domainName}";

            if (student is not null)
            {
                var user = new AppUser
                {
                    UserName = account,
                    Email = account,
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    studentId = studentId,

                };

                var result = await _userManager.CreateAsync(user, passWord.ToString());
                await _userManager.AddToRoleAsync(user, studanteRole);
            }

        }

        public async Task<AppUser> ReadUserByStudentId(int studentId) => await db.Users.FirstOrDefaultAsync(x => x.studentId == studentId);
    }
}
