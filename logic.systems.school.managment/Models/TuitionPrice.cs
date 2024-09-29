using System.ComponentModel.DataAnnotations.Schema;

namespace logic.systems.school.managment.Models
{  
    [Table("TuitionPrice")]
    public class TuitionPrice : Common
    {
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; } 
        public string Description { get; set; }

        public int Year { get; set; }

        public List<TuitionPriceHistory> TuitionPriceHistory { get; set; } = new List<TuitionPriceHistory>();

    }
}
