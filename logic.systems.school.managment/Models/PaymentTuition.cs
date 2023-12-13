using System.ComponentModel.DataAnnotations.Schema;
namespace logic.systems.school.managment.Models
{
    [Table("PaymentTuition")]
    public class PaymentTuition : Payment
    {
        public virtual Fines Fines { get; set; }
        public int FinesId { get; set; }
    }
}
