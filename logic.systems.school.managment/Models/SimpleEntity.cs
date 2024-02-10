using System.ComponentModel.DataAnnotations.Schema;

namespace logic.systems.school.managment.Models
{
    [Table("SimpleEntity")]
    public class SimpleEntity : Common
    {
        public const string Type_Subject = "Subject";
        public string Type { get; set; }       
        public string Description { get; set; }
    }
}
