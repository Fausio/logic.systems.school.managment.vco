using logic.systems.school.managment.Dto;
using logic.systems.school.managment.Models;

namespace logic.systems.school.managment.Interface
{
    public interface IGradeService : ICRUD<Grade>
    {
        public Task<PaginationDTO<Grade>> SearchRecord(string Name, int CurrentSchoolLevelId);
        public Task Create(List<Grade> models, string userId);
        public Task<List<Assessment>> ReadAssessmentsByClassLevelClassRoomSubjectQuarter(GradeConfigDTO dto);
        public Task<List<Assessment>> ReadAssessmentsByClassLevelClassRoomSubjectYear(GradeConfigDTO dto);
        public Task<GradeConfigDTO>? ReadGradeConfig(int professorConfigId,int   quarter, string ProfessorAccountId);
    }
}