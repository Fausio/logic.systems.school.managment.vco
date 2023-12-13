namespace logic.systems.school.managment.Interface
{
    public interface IEnrollment
    {
        public Task EnrollmentByStudantId(int  studantId, int CurrentSchoolLevelId);
    }
}
