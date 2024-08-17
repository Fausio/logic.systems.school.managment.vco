using System.ComponentModel.DataAnnotations.Schema;

namespace logic.systems.school.managment.Models
{ 
    [Table("EnrollmentPriceHistory")]
    public class EnrollmentPriceHistory : Common
    {
        public virtual EnrollmentPrice EnrollmentPrice { get; set; }
        public int EnrollmentPriceeId { get; set; }
        public string Description { get; set; }
    }

}
