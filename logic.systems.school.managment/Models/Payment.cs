using System.ComponentModel.DataAnnotations.Schema;

namespace logic.systems.school.managment.Models
{
    [Table("Payment")]
    public class Payment : Common
    {
        public bool Paid { get; set; }
        public DateTime PaymentDate { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal PaymentWithoutVat { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal VatOfPayment { get; set; } // 5%
        [Column(TypeName = "decimal(18, 2)")]
        public decimal PaymentWithVat { get; set; }
    }
}
