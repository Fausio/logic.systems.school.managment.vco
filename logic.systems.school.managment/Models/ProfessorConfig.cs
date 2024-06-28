using System.ComponentModel.DataAnnotations.Schema;

namespace logic.systems.school.managment.Models
{
    [Table("ProfessorConfig")]
    public class ProfessorConfig : Common
    {
        public string UserId { get; set; }
        public int? ClassLevelId { get; set; }
        public SimpleEntity? ClassLevel { get; set; }
        public int? ClassRoomId { get; set; }
        public SimpleEntity? ClassRoom { get; set; }
        public int? SubjectId { get; set; }
        public SimpleEntity? Subject { get; set; }
        public int EnrollmentYears { get; set; }
    }
}
