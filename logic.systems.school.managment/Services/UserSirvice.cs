using logic.systems.school.managment.Data;
using logic.systems.school.managment.Dto;
using logic.systems.school.managment.Interface;
using logic.systems.school.managment.Models;
using Microsoft.EntityFrameworkCore;

namespace logic.systems.school.managment.Services
{
    public class UserSirvice : IUserSirvice
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext(new DbContextOptions<ApplicationDbContext>());

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
    }
}
