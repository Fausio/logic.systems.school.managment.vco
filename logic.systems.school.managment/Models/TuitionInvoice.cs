using System.ComponentModel.DataAnnotations.Schema;

namespace logic.systems.school.managment.Models
{
    [Table("TuitionInvoice")]
    public class TuitionInvoice : Invoice
    {
        //public virtual Tuition? Tuition { get; set; }
        //public int? TuitionId { get; set; }
        public TuitionInvoice()
        {
            this.Type = "TuitionInvoice";
            this.Date = DateTime.Now;
        }

    }
}
