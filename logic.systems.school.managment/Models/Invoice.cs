using logic.systems.school.managment.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace logic.systems.school.managment.Models
{
    [Table("Invoice")]
    public class Invoice : Common
    {
        public DateTime  Date { get; set; }
        public string Type { get; set; }
    }
}
