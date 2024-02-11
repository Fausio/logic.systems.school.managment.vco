using System.ComponentModel.DataAnnotations.Schema;

namespace logic.systems.school.managment.Models
{
    [Table("ProductInvoice")]
    public class ProductInvoice : Invoice
    {

        public Invoice? Invoice { get; set; }
        public int? InvoiceId { get; set; }

        public DateTime Date { get; set; }
        public string Type { get; set; }

        public ProductInvoice()
        {
            this.Type = "ProductInvoice";
            this.Date = DateTime.Now;
        }
    }
}
