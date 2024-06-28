using logic.systems.school.managment.Dto;
using logic.systems.school.managment.Models;

namespace logic.systems.school.managment.Interface
{
    public interface IUserSirvice
    {
        public Task<PaginationDTO<AppUser>> SearchRecord(string searchString);
        public Task<PaginationDTO<AppUser>> ReadPagenation(int pageNumber = 1, int pageSize = 10);
    }
}
