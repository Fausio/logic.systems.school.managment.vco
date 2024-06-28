using logic.systems.school.managment.Models;

namespace logic.systems.school.managment.Dto
{
    public class ProfessorConfigCreateDTO
    {
        public string UserId { get; set; }
        public string SchoolLevels { get; set; }
        public string SchoolClassRooms { get; set; }
        public string Subjects { get; set; }
        public string EnrollmentYears { get; set; }


        public ProfessorConfig GetProfessorConfig(string createUserId)
        {
            var entity = new ProfessorConfig()
            {
                Id = 0,
                CreatedUSer = createUserId,
                CreatedDate = DateTime.Now, 
                UserId = UserId,
                ClassLevelId = int.Parse(SchoolLevels),
                ClassRoomId = int.Parse(SchoolClassRooms),
                SubjectId = int.Parse(Subjects),
                EnrollmentYears = int.Parse(EnrollmentYears),
            };

            return entity;
        }
    }
}
