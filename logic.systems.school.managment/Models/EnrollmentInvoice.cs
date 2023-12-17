using System.ComponentModel.DataAnnotations.Schema;

namespace logic.systems.school.managment.Models
{
    [Table("EnrollmentInvoice")]
    public class EnrollmentInvoice : Invoice
    {
        public virtual Enrollment Enrollment { get; set; }
        public int EnrollmentId { get; set; }

        public EnrollmentInvoice()
        { 
            this.Type = "EnrollmentInvoice";
            this.Date = DateTime.Now;
        }



    }
}
