using DocumentFormat.OpenXml.Spreadsheet;
using logic.systems.school.managment.Data;
using logic.systems.school.managment.Dto;
using logic.systems.school.managment.Interface;
using logic.systems.school.managment.Models;
using Microsoft.EntityFrameworkCore;

namespace logic.systems.school.managment.Services
{
    public class SalesService : ISalesService
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext(new DbContextOptions<ApplicationDbContext>());
        public Task<ProductPayment> Create(ProductPayment model, string CreatedById)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int modelID, string UpdatedById)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ProductInvoice>> GetgetProducts(int id) 
         =>    await  db.ProductInvoices.Include(x => x.ProductPayments)
                                                    .ThenInclude( x=> x.Product)
                                        .Where(x => x.StudentId == id)
                                        .ToListAsync();
         

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

        public async Task Sell(List<ProductSellDTO> data, string userId)
        {

            if (data.Count > 0)
            {
                var invoice = new ProductInvoice();
                var now = DateTime.Now;

                foreach (var item in data)  
                {
                    var prod = await  db.Products.FirstOrDefaultAsync(x => x.Id == int.Parse(item.product));
                    var quantity = int.Parse(item.quantity);
                    if (prod != null && quantity > 0)
                    {
                       
                        invoice.ProductPayments.Add(

                            new ProductPayment()
                            {
                                ProductId = prod.Id,
                                PaymentDate = now,
                                Paid = true,
                                Quantity = quantity,
                                CreatedDate = now,
                                CreatedUSer = userId,
                                PaymentWithVat = prod.Price * quantity,

                            });
                    }
                }

                invoice.StudentId = int.Parse(data.FirstOrDefault().studentId);
                invoice.Date = now;
                invoice.CreatedDate = now;
                invoice.CreatedUSer = userId;

                await db.ProductInvoices.AddAsync(invoice);
                await db.SaveChangesAsync();
            }
            

             
        }

        public Task<ProductPayment> Update(ProductPayment model, string UpdatedById)
        {
            throw new NotImplementedException();
        }
    }
}
