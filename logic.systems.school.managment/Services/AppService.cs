using logic.systems.school.managment.Data;
using logic.systems.school.managment.Interface;
using logic.systems.school.managment.Models;
using Microsoft.EntityFrameworkCore;

namespace logic.systems.school.managment.Services
{
    public class AppService : IApp
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext(new DbContextOptions<ApplicationDbContext>());

        public async Task<string> SempleEntityDescriptionById(int id)
        {
            var result = await db.SimpleEntitys.FirstOrDefaultAsync(x => x.Id == id); 
       
            if (result is not null)
            {
                return result.Description;
            }
            else
            {
                return "";
            }
        } 
         
        public async Task<bool> LimitOfStudentByClassRoomAndLevelYear(int EnrollmentYear, int CurrentSchoolLevelId, int SchoolClassRoomId)
        {


            // limit by classroom is 35 students

             var CountStudentByClassRoomAndLevelYear  = await db.Enrollments.CountAsync( 
                 x =>  
                        x.EnrollmentYear == EnrollmentYear  &&
                        x.SchoolLevelId == CurrentSchoolLevelId &&
                        x.SchoolClassRoomId == SchoolClassRoomId
                 );

            if (CountStudentByClassRoomAndLevelYear >= 35)
            {
                return true;
            }
            else
            {
                return false;
            }

           
        }
    }
}
