using System.ComponentModel.DataAnnotations.Schema;

namespace logic.systems.school.managment.Models
{
    [Table("Audit")]
    public class Audit
    {
        public int Id { get; set; }
        public string Action { get; set; } 
        public string ActionReason { get; set; }
        public int StudentId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedUSer { get; set; } 

    }
}
