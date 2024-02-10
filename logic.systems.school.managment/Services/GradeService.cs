using logic.systems.school.managment.Data;
using logic.systems.school.managment.Dto;
using logic.systems.school.managment.Interface;
using logic.systems.school.managment.Models;
using Microsoft.EntityFrameworkCore;

namespace logic.systems.school.managment.Services
{
    public class GradeService : IGradeService
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext(new DbContextOptions<ApplicationDbContext>());

        public Task Create(List<Grade> models, string userId)
        {
            throw new NotImplementedException();
        }

        public Task<Grade> Create(Grade model, string CreatedById)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int modelID, string UpdatedById)
        {
            throw new NotImplementedException();
        }

        public Task<Grade> Read(int modelID)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Assessment>> ReadAssessmentsByClassLevelClassRoomSubjectQuarter(GradeConfigDTO dto )
        {
            var results = await db.Assessments
                                  .Include(g => g.Grades)
                                  .Include(s => s.Subject)
                                 
                                  .Include(q => q.Quarter)
                                       .ThenInclude(q => q.Enrollment)
                                       .ThenInclude(q => q.Student)
                                   .Where(
                                              x => x.Quarter.Enrollment.SchoolLevelId == dto.ClassLevel &&
                                                   x.Quarter.Enrollment.SchoolClassRoomId == dto.ClassRoom &&
                                                   x.SubjectId == dto.Subject &&
                                                   x.Quarter.Number == dto.Quarter
                                          )
                                   .ToListAsync();
            



            return results;
        }

        public Task<PaginationDTO<Grade>> ReadPagenation(int pageNumber = 1, int pageSize = 10)
        {
            throw new NotImplementedException();
        }

        public Task<PaginationDTO<Grade>> SearchRecord(string Name, int CurrentSchoolLevelId)
        {
            throw new NotImplementedException();
        }

        public Task<PaginationDTO<Grade>> SearchRecord(string searchString)
        {
            throw new NotImplementedException();
        }

        public Task<Grade> Update(Grade model, string UpdatedById)
        {
            throw new NotImplementedException();
        }
    }
}
