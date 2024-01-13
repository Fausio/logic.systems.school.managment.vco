using System.ComponentModel.DataAnnotations.Schema;

namespace logic.systems.school.managment.Models
{
    [Table("TuitionFine")]
    public class TuitionFine : Common
    {
        public virtual Tuition Tuition { get; set; }
        public int TuitionId { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal FinesValue { get; set; }
        public TuitionFine()
        {
            FinesValue = 300;
        }
        public bool Paid { get; set; }
        public DateTime? PaidDate { get; set; }
    }
}
