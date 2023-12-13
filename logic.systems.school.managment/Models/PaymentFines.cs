using System.ComponentModel.DataAnnotations.Schema;

namespace logic.systems.school.managment.Models
{
    [Table("PaymentFine")]
    public class PaymentFines :   Payment
    { 
        public virtual Fines Fines { get; set; }
        public int FinesId { get; set; }
    }
}
