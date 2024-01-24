using System.ComponentModel.DataAnnotations.Schema;

namespace logic.systems.school.managment.Models
{
    [Table("TuitionFineDaily")]
    public class TuitionFineDaily : Common
    {
        public TuitionFine TuitionFine { get; set; }
        public int TuitionFineId { get; set; }

        public bool Paid { get; set; }
        public DateTime? PaidDate { get; set; }

        public decimal FinesValue { get; set; }
        public DateTime FinesDate { get; set; }

        public TuitionFineDaily()
        {
            FinesValue = 25;
        }
    }
}
