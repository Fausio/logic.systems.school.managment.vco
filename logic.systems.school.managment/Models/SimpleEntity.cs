using System.ComponentModel.DataAnnotations.Schema;

namespace logic.systems.school.managment.Models
{
    [Table("SimpleEntity")]
    public class SimpleEntity : Common
    {
        public string type { get; set; }       
        public string Description { get; set; }
    }
}
