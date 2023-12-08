using System.ComponentModel.DataAnnotations.Schema;

namespace logic.systems.school.managment.Models
{
    [Table("Payment")]
    public class Payment : Common
    {

        public virtual Tuition Tuition { get; set; }
        public int TuitionId { get; set; }

        public DateTime PaymentDate { get; set; }
        public decimal MonthlyFeeWithoutVat { get; set; }
        public decimal VatOfMonthlyFee { get; set; } // 5%
        public decimal MonthlyFeeWithVat { get; set; }
    }
}
