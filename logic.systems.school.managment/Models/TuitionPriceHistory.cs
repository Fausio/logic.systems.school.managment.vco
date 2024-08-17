using System.ComponentModel.DataAnnotations.Schema;

namespace logic.systems.school.managment.Models
{
    [Table("TuitionPriceHistory")]
    public class TuitionPriceHistory : Common
    {
        public virtual TuitionPrice TuitionPrice { get; set; }
        public int TuitionPriceId { get; set; } 
        public  string Description { get; set; }
    }
}
