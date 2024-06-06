using System.ComponentModel.DataAnnotations.Schema;

namespace logic.systems.school.managment.Models
{
    [Table("EnrollmentInvoice")]
    public class EnrollmentInvoice : Common
    {
        public virtual Enrollment Enrollment { get; set; }
        public int EnrollmentId { get; set; }


        public Invoice? Invoice { get; set; }
        public int? InvoiceId { get; set; }

        public DateTime Date { get; set; }
        public string Type { get; set; }

        public EnrollmentInvoice()
        { 
            this.Type = "EnrollmentInvoice";
            this.Date = DateTime.Now;
        }



    }
}
