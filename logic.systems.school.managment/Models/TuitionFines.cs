using System.ComponentModel.DataAnnotations.Schema;

namespace logic.systems.school.managment.Models
{
    [Table("TuitionFines")]
    public class TuitionFines : Common
    {
        public virtual Tuition Tuition { get; set; }
        public int TuitionId { get; set; }

        public decimal FinesValue { get; set; }

        public TuitionFines()
        {
            FinesValue = 300;
        }
    }
}
