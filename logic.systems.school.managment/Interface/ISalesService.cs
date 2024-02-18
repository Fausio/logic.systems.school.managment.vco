using logic.systems.school.managment.Dto;
using logic.systems.school.managment.Models;

namespace logic.systems.school.managment.Interface
{
    public interface ISalesService : ICRUD<ProductPayment>
    {
        public Task<List<ProductInvoice>> GetgetProducts(int id);
        public Task Sell(List<ProductSellDTO> data, string id);
    }
}
