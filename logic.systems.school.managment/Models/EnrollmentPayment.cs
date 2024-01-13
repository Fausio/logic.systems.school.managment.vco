using System.ComponentModel.DataAnnotations.Schema;

namespace logic.systems.school.managment.Models
{
    [Table("EnrollmentPayment")]
    public class EnrollmentPayment : Payment
    {
        public virtual Enrollment Enrollment { get; set; }
        public int EnrollmentId { get; set; } 

    }
}
