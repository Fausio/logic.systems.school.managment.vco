using System.ComponentModel.DataAnnotations.Schema;

namespace logic.systems.school.managment.Models
{
    [Table("ProductPayment")]
    public class ProductPayment : Payment
    { 
        public virtual Product Enrollment { get; set; }
        public int ProductId { get; set; }

        public int Quantity { get; set; }
    }
}
