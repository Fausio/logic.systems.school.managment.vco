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

        public async Task Create(List<Grade> models, string userId)
        {
            var now = DateTime.Now;
            var listOfGrades = new List<Grade>();

            foreach (var item in models)
            {
                var model = await Read(item.Id);
                model.UpdatedDate = now;
                model.UpdatedUSer = userId;

                model.Value = item.Value;
                listOfGrades.Add(model);

                var assessment = await db.Assessments.FirstOrDefaultAsync(x => x.Id == item.AssessmentId);

                if (assessment is not null)
                {
                    assessment.UpdatedDate = now;
                    assessment.UpdatedUSer = userId;

                    db.Assessments.Update(assessment);
                }
            }

            db.Grades.UpdateRange(listOfGrades);
            await db.SaveChangesAsync();
        }

        public Task<Grade> Create(Grade model, string CreatedById)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int modelID, string UpdatedById)
        {
            throw new NotImplementedException();
        }

        public async Task<Grade> Read(int modelID) => await db.Grades.FirstOrDefaultAsync(x => x.Id == modelID);

        public async Task<List<Assessment>> ReadAssessmentsByClassLevelClassRoomSubjectYear(GradeConfigDTO dto)
        {
            var results = await db.Assessments
                                  .Include(g => g.Grades)
                                  .Include(s => s.Subject)

                                  .Include(q => q.Quarter)
                                       .ThenInclude(q => q.Enrollment)
                                       .ThenInclude(q => q.Student)
                                   .Where(
                                              x => x.Quarter.Enrollment.SchoolLevelId == dto.ClassLevel &&
                                                   x.Quarter.Enrollment.EnrollmentYear == dto.EnrollmentYears &&
                                                   x.Quarter.Enrollment.SchoolClassRoomId == dto.ClassRoom &&
                                                   x.SubjectId == dto.Subject &&
                                                   x.Quarter.Enrollment.Student.Row != Common.Deleted ||
                                                   x.Quarter.Enrollment.Student.Suspended
                                          )
                                   .ToListAsync();




            return results;
        }
        public async Task<List<Assessment>> ReadAssessmentsByClassLevelClassRoomSubjectQuarter(GradeConfigDTO dto)
        {
            var results = await db.Assessments
                                  .Include(g => g.Grades)
                                  .Include(s => s.Subject)

                                  .Include(q => q.Quarter)
                                       .ThenInclude(q => q.Enrollment)
                                       .ThenInclude(q => q.Student)
                                   .Where(
                                              x => x.Quarter.Enrollment.SchoolLevelId == dto.ClassLevel &&
                                                   x.Quarter.Enrollment.EnrollmentYear == dto.EnrollmentYears &&
                                                   x.Quarter.Enrollment.SchoolClassRoomId == dto.ClassRoom &&
                                                   x.SubjectId == dto.Subject &&
                                                   x.Quarter.Number == dto.Quarter &&
                                                   x.Quarter.Enrollment.Student.Row != Common.Deleted ||
                                                   x.Quarter.Enrollment.Student.Suspended
                                          )
                                   .OrderBy(x => x.Quarter.Enrollment.Student.Name)
                                   .ToListAsync();

            results.ForEach(async x =>
            {

                if (x.CreatedUSer != null && x.CreatedUSer != "logicsystems.co.mz")
                {
                    var user = await db.Users.FirstAsync(u => u.Id == x.CreatedUSer);
                    x.CreatedUSer = user.UserName;
                }
            });

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

        public async Task<GradeConfigDTO>? ReadGradeConfig(int professorConfigId, int quarter, string ProfessorAccountId)
        {
            return await db.ProfessorConfig.Where(x => x.Id == professorConfigId && x.UserId == ProfessorAccountId)
                           .Select(x => new GradeConfigDTO()
                           {
                               ClassLevel = x.ClassLevelId.Value,
                               ClassRoom = x.ClassRoomId.Value,
                               Subject = x.SubjectId.Value,
                               Quarter = quarter,
                               EnrollmentYears = x.EnrollmentYears,

                           }).FirstOrDefaultAsync();
        }
    }
}
