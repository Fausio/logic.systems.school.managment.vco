﻿using logic.systems.school.managment.Data;
using logic.systems.school.managment.Dto;
using logic.systems.school.managment.Interface;
using logic.systems.school.managment.Models;
using Microsoft.EntityFrameworkCore;

namespace logic.systems.school.managment.Services
{
    public class StudantService : ICRUD<Student>
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext(new DbContextOptions<ApplicationDbContext>());
        public async Task<Student> Create(Student model, string CreatedById)
        {
            try
            {
                model.CreatedUSer = CreatedById;
                await db.Students.AddAsync(model);
                await db.SaveChangesAsync();
                return model;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task Delete(int modelID, string UpdatedById)
        {
            try
            {
                var model = await Read(modelID);
                model.Row = Common.Deleted;
                await Update(model, UpdatedById);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Student> Read(int modelID)
        {
            try
            {
                return await db.Students.FirstOrDefaultAsync(x => x.Id == modelID && x.Row != Common.Deleted);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<PaginationDTO<Student>> ReadPagenation(int pageNumber = 1, int pageSize = 10)
        {
            pageNumber = (pageNumber <= 0) ? 1 : pageNumber;
            pageSize = (pageSize <= 0) ? 10 : pageSize;


            var totalRecords = await db.Students.CountAsync<Student>();
            var totalPages = Math.Ceiling((double)totalRecords / pageSize);


            var skip = (pageNumber - 1) * pageSize;


            var models = new List<Student>();


            models = await db.Students.Where(x => x.Row != Common.Deleted)
                                         .Skip(skip)
                                         .Take(pageSize)
                                         .Select(u => new Student()
                                         {
                                             Id = u.Id,
                                             Name = u.Name, 


                                         }).OrderBy(o => o.Name).ToListAsync();



            var records = new PaginationDTO<Student>()
            {
                pageNumber = pageNumber,
                pageSize = pageSize,
                totalRecords = totalRecords,
                totalPages = totalPages,
                records = models,

            };


            return records;
        }

        public Task<PaginationDTO<Student>> SearchRecord(string searchString)
        {
            throw new NotImplementedException();
        }

        public async Task<Student> Update(Student model, string UpdatedById)
        {
            try
            {
                if (model.Id > 0)
                {

                    model.UpdatedDate = DateTime.UtcNow;
                    model.UpdatedUSer = UpdatedById;
                    model.Row = Common.Modified;

                    db.Students.Update(model);
                    await db.SaveChangesAsync();
                    return model;
                }

                return null;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
