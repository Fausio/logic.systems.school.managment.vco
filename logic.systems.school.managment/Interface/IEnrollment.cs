using logic.systems.school.managment.Dto;
using logic.systems.school.managment.Models;

namespace logic.systems.school.managment.Interface
{
    public interface IEnrollment
    {
        public Task<Enrollment> EnrollmentByStudantId(int studantId, int CurrentSchoolLevelId, int EnrollmentYear, int SchoolClassRoomId);
        public Task<List<EnrollmentListDTO>> EnrollmentsByStudantId(int studantId);
        public Task<List<EnrollmentListDTO>> EnrollmentsByStudantId(EnrollmentCreateDTO model, string userId);  
        public Task<bool> CheckIfHaveEnrollmentIntheYear(EnrollmentCreateDTO model);

        public Task DeleteParmanentyById(int Id);

        public  Task<List<EnrollmentPrice>> ReadEnrolmentPrices();
        public Task<EnrollmentPrice> ReadEnrolmentPriceById(int id);
        public Task<EnrollmentPrice> ReadEnrolmentPriceByDescription(string description, int yearId);
        public Task<EnrollmentPrice> UpdateEnrollmentPrice(EnrollmentPrice entity);
      
        public Task<List<YearDefinition>> ReadYearDefinitions();

        public Task generateEnrolmentPrice(int YearIdid);
    }
}
