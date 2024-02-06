using logic.systems.school.managment.Dto;
using logic.systems.school.managment.Models;

namespace logic.systems.school.managment.Interface
{
    public interface IstudantService : ICRUD<Student>
    {
        public Task<PaginationDTO<Student>> SearchRecord(string Name, int CurrentSchoolLevelId);

        public Task<bool> CheckIfExists(string PersonalId);
        public Task Transfer(int id, string userId);
       public Task UpdateEnrollment(FixEnrollmentDTO dto, string updatedUser);

        public Task Delete(DeleteStudentDTO dto, string userId);
    }
}