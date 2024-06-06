using System.ComponentModel.DataAnnotations.Schema;

namespace logic.systems.school.managment.Models
{
    [Table("ProductInvoice")]
    public class ProductInvoice : Invoice
    {

        public Invoice? Invoice { get; set; }
        public int? InvoiceId { get; set; }

        public virtual Student? Student { get; set; }
        public int? StudentId { get; set; }

        public DateTime Date { get; set; }
        public string Type { get; set; }

        public ProductInvoice()
        {
            this.Type = "ProductInvoice";
            this.Date = DateTime.Now;
        }

        public List<ProductPayment> ProductPayments { get; set; } = new List<ProductPayment>();


        public string GetProducts()   => string.Join(", ", ProductPayments.Select(x => x.Product.Description));
         
        public decimal GetProductsPrice() => ProductPayments.Sum(x => x.PaymentWithVat);
    }
}
