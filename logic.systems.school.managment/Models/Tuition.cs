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

        public int StudentId { get; set; }
        public int AssociatedLevelId { get; set; }   
        public bool Paid { get; set; }

        [NotMapped]
        public bool Create { get; set; }

    }
}


 