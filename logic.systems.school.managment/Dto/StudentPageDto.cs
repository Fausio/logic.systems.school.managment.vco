namespace logic.systems.school.managment.Dto
{
    public class StudentPageDto
    {
        public string? studentName { get; set; }
        public int? CurrentSchoolLevelId { get; set; }
        public PaginationDTO<Models.Student> indexPage { get; set; }
    }
}
