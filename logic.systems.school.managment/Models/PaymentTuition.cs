using System.ComponentModel.DataAnnotations.Schema;
namespace logic.systems.school.managment.Models
{
    [Table("PaymentTuition")]
    public class PaymentTuition : Payment
    {
        public virtual Tuition Tuition { get; set; }
        public int TuitionId { get; set; }
    }
}
