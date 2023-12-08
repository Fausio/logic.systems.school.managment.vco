using System.ComponentModel.DataAnnotations.Schema;

namespace logic.systems.school.managment.Models
{
    [Table("Tuition")]
    public class Tuition : Common
    {
        public int MonthNumber { get; set; }
        public string MonthName { get; set; }
        public int Year { get; set; }
        public int StudentId { get; set; }
        public int AssociatedLevelId { get; set; }    
    }
}


 