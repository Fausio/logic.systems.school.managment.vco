using System.ComponentModel.DataAnnotations.Schema;

namespace logic.systems.school.managment.Models
{
    [Table("PaymentFine")]
    public class PaymentFines :   Payment
    {
        public virtual Tuition Tuition { get; set; }
        public int TuitionId { get; set; }
    }
}
