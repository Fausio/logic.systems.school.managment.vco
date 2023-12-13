using System.ComponentModel.DataAnnotations.Schema;

namespace logic.systems.school.managment.Models
{
    [Table("Enrollment")]
    public class Enrollment : Common
    {  
        public List<Tuition> Tuitions { get; set; } = new List<Tuition>();
    }
}
