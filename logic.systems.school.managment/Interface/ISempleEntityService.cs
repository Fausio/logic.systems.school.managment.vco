using logic.systems.school.managment.Models;

namespace logic.systems.school.managment.Interface
{
    public interface ISempleEntityService
    {

        public Task<List<SimpleEntity>> GetByType(string type);
        public Task<List<SimpleEntity>> GetByTypeOrderByDescription(string type);
        public Task<List<SimpleEntity>> GetByTypeOrderById(string type);   
        public Task<List<SimpleEntity>> GetGetSchoolClassRoomsBySchoolLevelId(int schoolLevelId);
    }
}
