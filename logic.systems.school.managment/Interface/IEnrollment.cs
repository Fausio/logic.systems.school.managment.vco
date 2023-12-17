using logic.systems.school.managment.Dto;
using logic.systems.school.managment.Models;

namespace logic.systems.school.managment.Interface
{
    public interface IEnrollment
    {
        public Task<Enrollment> EnrollmentByStudantId(int  studantId, int CurrentSchoolLevelId, int EnrollmentYear);
        public Task<List<EnrollmentListDTO>> EnrollmentsByStudantId(int  studantId );
    }
}
