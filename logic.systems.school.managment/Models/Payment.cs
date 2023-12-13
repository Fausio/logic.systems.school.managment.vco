using System.ComponentModel.DataAnnotations.Schema;

namespace logic.systems.school.managment.Models
{
    [Table("Payment")]
    public class Payment : Common
    {

        public virtual Tuition Tuition { get; set; }
        public int TuitionId { get; set; }
        public string type { get; set; }
        public DateTime PaymentDate { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal MonthlyFeeWithoutVat { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal VatOfMonthlyFee { get; set; } // 5%
        [Column(TypeName = "decimal(18, 2)")]
        public decimal MonthlyFeeWithVat { get; set; }
    }
}
