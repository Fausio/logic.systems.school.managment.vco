using System.ComponentModel.DataAnnotations.Schema;

namespace logic.systems.school.managment.Models
{
    [Table("TuitionInvoice")]
    public class TuitionInvoice : Common
    {
        public Invoice? Invoice { get; set; }
        public int? InvoiceId { get; set; }

        public DateTime Date { get; set; }
        public string Type { get; set; }

        public TuitionInvoice()
        {
            this.Type = "TuitionInvoice";
            this.Date = DateTime.Now;
        }

    }
}
