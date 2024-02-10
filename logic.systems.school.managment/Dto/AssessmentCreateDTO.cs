using logic.systems.school.managment.Models;

namespace logic.systems.school.managment.Dto
{
    public class AssessmentCreateDTO
    {
       
        public GradeConfigDTO dto { get; set; }   = new GradeConfigDTO();

        public List<Assessment> Assessments { get; set; }   
    }
}
