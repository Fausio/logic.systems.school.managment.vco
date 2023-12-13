using System.ComponentModel.DataAnnotations.Schema;

namespace logic.systems.school.managment.Models
{
    [Table("TuitionFines")]
    public class TuitionFines : Common
    {
        public virtual Tuition Tuition { get; set; }
        public int TuitionId { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal FinesValue { get; set; }

        public TuitionFines()
        {
            FinesValue = 300;
        }

        public bool Paid { get; set; }

        public DateTime? PaidDate { get; set; }
    }
}
