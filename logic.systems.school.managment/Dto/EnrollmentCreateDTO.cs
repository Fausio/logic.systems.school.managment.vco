using logic.systems.school.managment.Migrations;

namespace logic.systems.school.managment.Dto
{
    public class EnrollmentCreateDTO
    { 
        public int StudantId { get; set; }
        public int SchoolLevelId { get; set; }
        public int SchoolClassRoomId { get; set; }
        public int EnrollmentYear { get; set; }
    }
}
