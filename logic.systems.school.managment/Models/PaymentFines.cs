using System.ComponentModel.DataAnnotations.Schema;

namespace logic.systems.school.managment.Models
{
    [Table("PaymentFine")]
    public class PaymentFines :   Payment
    {
        public virtual Enrollment Enrollment { get; set; }
        public int EnrollmentId { get; set; }

        public virtual TuitionFine Fines { get; set; }
        public int FinesId { get; set; }
    }
}
