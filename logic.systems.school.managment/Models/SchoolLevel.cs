using System.ComponentModel.DataAnnotations.Schema;

namespace logic.systems.school.managment.Models
{
    [Table("SchoolLevel")]
    public class SchoolLevel : Common
    {
        public string LevelName { get; set; }
    }
}
