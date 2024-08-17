using System.ComponentModel.DataAnnotations.Schema;

namespace logic.systems.school.managment.Models
{
    [Table("EnrollmentItemstPrice")]
    public class EnrollmentItemstPrice : Common
    {
        public string Description { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }

        public virtual EnrollmentPrice EnrollmentPrice { get; set; }
        public int EnrollmentPriceId { get; set; }

    }
}
