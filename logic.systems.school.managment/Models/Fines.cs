using System.ComponentModel.DataAnnotations.Schema;

namespace logic.systems.school.managment.Models
{
    [Table("Fines")]
    public class Fines : Common
    {
        public virtual Tuition Tuition { get; set; }
        public int TuitionId { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal FinesValue { get; set; }
        public Fines()
        {
            FinesValue = 300;
        }
        public bool Paid { get; set; }
        public DateTime? PaidDate { get; set; }
    }
}
