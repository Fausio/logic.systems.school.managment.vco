using System.ComponentModel.DataAnnotations.Schema;

namespace logic.systems.school.managment.Models
{
    [Table("PaymentEnrollment")]
    public class PaymentEnrollment : Payment
    {
        public virtual Enrollment Enrollment { get; set; }
        public int EnrollmentId { get; set; } 

    }
}
