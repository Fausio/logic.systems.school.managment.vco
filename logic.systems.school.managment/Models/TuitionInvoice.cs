using System.ComponentModel.DataAnnotations.Schema;

namespace logic.systems.school.managment.Models
{
    [Table("TuitionInvoice")]
    public class TuitionInvoice : Invoice
    {
        public TuitionInvoice()
        {
            this.Type = "TuitionInvoice";
            this.Date = DateTime.Now;
        }

    }
}
