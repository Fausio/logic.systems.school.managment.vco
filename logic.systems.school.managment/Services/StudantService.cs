using DocumentFormat.OpenXml.Wordprocessing;
using logic.systems.school.managment.Data;
using logic.systems.school.managment.Dto;
using logic.systems.school.managment.Interface;
using logic.systems.school.managment.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Printing;

namespace logic.systems.school.managment.Services
{
    public class StudantService : IstudantService
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext(new DbContextOptions<ApplicationDbContext>());

        public async Task<bool> CheckIfExists(string PersonalId)
        {
            try
            {
                Student student = await db.Students.FirstOrDefaultAsync(x => x.PersonId == PersonalId);

                if (student is not null)
                {
                    return true;
                }

                return false;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Student> Create(Student model, string CreatedById)
        {
            try
            {
                model.CreatedUSer = CreatedById;
                await db.Students.AddAsync(model);
                await db.SaveChangesAsync();
                return await Read(model.Id);
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
                // pegar sempre o mais recente
                var result = await db.Students.Include(x => x.CurrentSchoolLevel)
                                        .Include(x => x.District).ThenInclude(x => x.OrgUnitProvince)
                                        .Include(x => x.Enrollments).ThenInclude(t => t.SchoolLevel) 
                                        .Include(x => x.Enrollments).ThenInclude(t => t.Tuitions)
                                        .Include(x => x.Sponsor)
                                        .ThenInclude(x => x.Contacts)
                                        .FirstOrDefaultAsync(x => x.Id == modelID && x.Row != Common.Deleted);

                return result;
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


            models = await db.Students.Include(x => x.SchoolClassRoom).Include(x => x.Enrollments).ThenInclude(x => x.Tuitions).ThenInclude(x => x.TuitionFines).Include(x => x.CurrentSchoolLevel).Where(x => x.Row != Common.Deleted)
                                         .Skip(skip)
                                         .Take(pageSize)
                                         .Select(u => new Student()
                                         {
                                             Id = u.Id,
                                             Name = u.Name,
                                             CurrentSchoolLevel = u.CurrentSchoolLevel,
                                             SchoolClassRoom = u.SchoolClassRoom,
                                             Suspended = u.Suspended,
                                             Transferred = u.Transferred,
                                             haveFee = u.Enrollments.Any(x => x.Tuitions.Any(x => !x.TuitionFines.Paid))

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

        public async Task<PaginationDTO<Student>> SearchRecord(string Name, int CurrentSchoolLevelId)
        {
            var data = new PaginationDTO<Student>();


            if (!string.IsNullOrEmpty(Name) && CurrentSchoolLevelId > 0)
            {
                data.records = await db.Students.Include(x => x.Enrollments)
                                        .ThenInclude(x => x.Tuitions)
                                        .ThenInclude(x => x.TuitionFines)
                                        .Include(x => x.CurrentSchoolLevel)
                                        .Where(x => x.Row != Common.Deleted && x.Name.Contains(Name) && x.CurrentSchoolLevelId == CurrentSchoolLevelId)
                                         .Select(u => new Student()
                                         {
                                             Id = u.Id,
                                             Name = u.Name,
                                             CurrentSchoolLevel = u.CurrentSchoolLevel,
                                             SchoolClassRoom = u.SchoolClassRoom,
                                             Suspended = u.Suspended,
                                             haveFee = u.Enrollments.Any(x => x.Tuitions.Any(x => !x.TuitionFines.Paid))
                                         }).OrderBy(o => o.Name).ToListAsync();
            }
            else if (!string.IsNullOrEmpty(Name) && CurrentSchoolLevelId <= 0)
            {
                data.records = await db.Students.Include(x => x.Enrollments)
                                        .ThenInclude(x => x.Tuitions)
                                        .ThenInclude(x => x.TuitionFines)
                                        .Include(x => x.CurrentSchoolLevel)
                                        .Where(x => x.Row != Common.Deleted && x.Name.Contains(Name))
                                         .Select(u => new Student()
                                         {
                                             Id = u.Id,
                                             Name = u.Name,
                                             CurrentSchoolLevel = u.CurrentSchoolLevel,
                                             SchoolClassRoom = u.SchoolClassRoom,
                                             Suspended = u.Suspended,
                                             haveFee = u.Enrollments.Any(x => x.Tuitions.Any(x => !x.TuitionFines.Paid))
                                         }).OrderBy(o => o.Name).ToListAsync();

            }
            else if (string.IsNullOrEmpty(Name) && CurrentSchoolLevelId > 0)
            {
                data.records = await db.Students.Include(x => x.Enrollments)
                                        .ThenInclude(x => x.Tuitions)
                                        .ThenInclude(x => x.TuitionFines)
                                        .Include(x => x.CurrentSchoolLevel)
                                        .Where(x => x.Row != Common.Deleted && x.CurrentSchoolLevelId == CurrentSchoolLevelId)
                                         .Select(u => new Student()
                                         {
                                             Id = u.Id,
                                             Name = u.Name,
                                             CurrentSchoolLevel = u.CurrentSchoolLevel,
                                             SchoolClassRoom = u.SchoolClassRoom,
                                             Suspended = u.Suspended,
                                             haveFee = u.Enrollments.Any(x => x.Tuitions.Any(x => !x.TuitionFines.Paid))
                                         }).OrderBy(o => o.Name).ToListAsync();
            }
            else
            {
                data = await ReadPagenation(1, 20);
            }

            return data;
        }

        public async Task Transfer(int id, string userId)
        {
            try
            {
                Student student = await Read(id);
                student.Transferred = true;
           await     Update(student, userId);

               
            }
            catch (Exception)
            {

                throw;
            }
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

        public async Task UpdateEnrollment(FixEnrollmentDTO dto, string updatedUser)
        {
            var result = await Read(dto.StudantId);

            if (result is not null)
            {
                result.CurrentSchoolLevelId = int.Parse( dto.NewSchoolLevelId); 
                result.Internal = dto.NewchkInternal;
                result.SchoolClassRoomId = int.Parse(dto.NewSchoolClassRoomId);

                await Update(result, updatedUser);
            }
           
        }
    }
}
