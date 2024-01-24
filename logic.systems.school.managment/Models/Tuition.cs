using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations.Schema;

namespace logic.systems.school.managment.Models
{
    [Table("Tuition")]
    public class Tuition : Common
    {
        public int MonthNumber { get; set; }
        public string MonthName { get; set; }
        public int Year { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        [Required]
        public int StudentId { get; set; }
        public int AssociatedLevelId { get; set; }
        public bool Paid { get; set; }
        public DateTime? PaidDate { get; set; }

        [NotMapped]
        public bool Create { get; set; }

        public TuitionFine? TuitionFines { get; set; } 
        public virtual Enrollment? Enrollment { get; set; }
        public int? EnrollmentId { get; set; }


    }
}


