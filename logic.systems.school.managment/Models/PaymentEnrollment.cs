using System.ComponentModel.DataAnnotations.Schema;

namespace logic.systems.school.managment.Models
{
    [Table("PaymentTuition")]
    public class PaymentEnrollment : Payment
    {
        public virtual Enrollment Enrollment { get; set; }
        public int EnrollmentId { get; set; }
    }
}
