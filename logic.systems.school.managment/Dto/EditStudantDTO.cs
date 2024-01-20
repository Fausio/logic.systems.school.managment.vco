using logic.systems.school.managment.Models;

namespace logic.systems.school.managment.Dto
{
    public class EditStudantDTO:CreateStudantDTO
    {
        public int id { get; set; }
        public bool  Suspended { get; set; }
        public bool Transferred { get; set; }
 
        public int age { get; set; }

        public List<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
 
    }
}
