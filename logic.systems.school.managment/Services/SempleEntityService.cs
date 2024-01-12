﻿using logic.systems.school.managment.Data;
using logic.systems.school.managment.Interface;
using logic.systems.school.managment.Models;
using Microsoft.EntityFrameworkCore;

namespace logic.systems.school.managment.Services
{
    public class SempleEntityService : ISempleEntityService
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext(new DbContextOptions<ApplicationDbContext>());

        public async Task<List<SimpleEntity>> GetByType(string type)
        {
            try
            {
                return await db.SimpleEntitys.Where(x => x.Type == type).ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<SimpleEntity>> GetByTypeOrderByDescription(string type)
        {
            try
            {
                var result = await GetByType(type);
                return result.OrderBy(x => x.Description).ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<SimpleEntity>> GetByTypeOrderById(string type)
        {
            try
            {
                var result = await GetByType(type);
                return result.OrderBy(x => x.Id).ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<SimpleEntity>> GetGetSchoolClassRoomsBySchoolLevelId(int schoolLevelId)
        {
            var results = await GetByTypeOrderById("SchoolClassRoom"); 
            var SchoolLevel = await db.SimpleEntitys.FirstOrDefaultAsync(x => x.Id == schoolLevelId);



            if ("12ª classe" == SchoolLevel.Description)
            {
                return results.Where(x => x.Description.Contains("Grupo")).ToList();
            }
            else
            {
                return results.Where(x => x.Description.Contains("Turma")).ToList();
            }
        }
    }
}