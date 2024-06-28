using System.ComponentModel.DataAnnotations.Schema;

namespace logic.systems.school.managment.Models
{
    [Table("ProfessorConfig")]
    public class ProfessorConfig : Common
    {
        public string UserId { get; set; }
        public int ClassLevel { get; set; }
        public int ClassRoom { get; set; }
        public int Subject { get; set; }
        public int EnrollmentYears { get; set; }
    }
}
