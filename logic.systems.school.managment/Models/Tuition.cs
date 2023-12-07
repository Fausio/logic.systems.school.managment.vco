using System.ComponentModel.DataAnnotations.Schema;

namespace logic.systems.school.managment.Models
{
    [Table("Tuition")]
    public class Tuition : Common
    {
         
        public int AssociatedLevelId { get; set; } 
        public int StudentId { get; set; } 

        public DateTime TuitionDate { get; set; }
        public decimal MonthlyFeeWithoutVat { get; set; }
        public decimal VatOfMonthlyFee { get; set; } // 5%
        public decimal MonthlyFeeWithVat { get; set; }
    }
}
