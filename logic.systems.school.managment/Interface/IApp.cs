using DocumentFormat.OpenXml.Bibliography;
using logic.systems.school.managment.Models;

namespace logic.systems.school.managment.Interface
{
    public interface IApp
    {
        public Task<bool> LimitOfStudentByClassRoomAndLevelYear(int EnrollmentYear, int CurrentSchoolLevelId, int SchoolClassRoomId);

        public Task<string> SempleEntityDescriptionById(int id); 
    }
}
