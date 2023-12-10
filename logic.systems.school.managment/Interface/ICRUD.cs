using logic.systems.school.managment.Dto;

namespace logic.systems.school.managment.Interface
{
    public interface ICRUD<T> 
    {
        public Task<T> Create(T model, string CreatedById);
        public Task<PaginationDTO<T>> ReadPagenation(int pageNumber = 1, int pageSize = 10);
        public Task<PaginationDTO<T>> SearchRecord(string searchString);
        public Task<T> Read(int modelID);
        public Task<T> Update(T model, string UpdatedById);
        public Task Delete(int modelID, string UpdatedById);
    }
}
