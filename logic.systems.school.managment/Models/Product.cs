using System.ComponentModel.DataAnnotations.Schema;

namespace logic.systems.school.managment.Models
{
    [Table("Product")]
    public class Product : Common
    {
        public string Description { get; set; } 
        public decimal Price { get; set; }
    }
}
