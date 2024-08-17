using System.ComponentModel.DataAnnotations.Schema;

namespace logic.systems.school.managment.Models
{
    [Table("RevertTuition")]
    public class RevertTuition : Common
    {
        public int MonthNumber { get; set; }
        public string MonthName { get; set; }
        public int Year { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
         
        public int StudentId { get; set; }
        public int AssociatedLevelId { get; set; } 
          
        public int? EnrollmentId { get; set; }

        public DateTime PaymentDate { get; set; }

        [NotMapped]
        public string _PaymentDate { get; set; }

        [NotMapped]
        public string _CreatedDate { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal PaymentWithoutVat { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal VatOfPayment { get; set; } // 5%
        [Column(TypeName = "decimal(18, 2)")]
        public decimal PaymentWithVat { get; set; }
    }
}
