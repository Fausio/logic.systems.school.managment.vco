using logic.systems.school.managment.Dto;
using logic.systems.school.managment.Interface;
using logic.systems.school.managment.Models;

namespace logic.systems.school.managment.Services
{
    public class SalesService : ISalesService
    {
        public Task<ProductPayment> Create(ProductPayment model, string CreatedById)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int modelID, string UpdatedById)
        {
            throw new NotImplementedException();
        }

        public Task<ProductPayment> Read(int modelID)
        {
            throw new NotImplementedException();
        }

        public Task<PaginationDTO<ProductPayment>> ReadPagenation(int pageNumber = 1, int pageSize = 10)
        {
            throw new NotImplementedException();
        }

        public Task<PaginationDTO<ProductPayment>> SearchRecord(string searchString)
        {
            throw new NotImplementedException();
        }

        public Task<ProductPayment> Update(ProductPayment model, string UpdatedById)
        {
            throw new NotImplementedException();
        }
    }
}
