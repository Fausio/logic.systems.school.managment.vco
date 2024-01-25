namespace logic.systems.school.managment.Dto
{
    public class FixEnrollmentDTO
    {

        public int StudantId { get; set; }
        public string OldEnrollment { get; set; }
        public string NewSchoolLevelId { get; set; }
        public string NewSchoolClassRoomId { get; set; }
        public string NewEnrollmentYear { get; set; }
        public bool NewchkInternal { get; set; }

    }
}
