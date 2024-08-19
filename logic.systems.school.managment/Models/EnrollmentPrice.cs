using logic.systems.school.managment.Migrations;
using System.ComponentModel.DataAnnotations.Schema;

namespace logic.systems.school.managment.Models
{
    [Table("EnrollmentPrice")]
    public class EnrollmentPrice : Common
    {
        public YearDefinition? YearDefinition { get; set; }
        public int? YearDefinitionId { get; set; }


        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; } 
        public string Description { get; set; }
        public List<EnrollmentItemstPrice> EnrollmentItemstPrice { get; set; } = new List<EnrollmentItemstPrice>();
        public List<EnrollmentPriceHistory> EnrollmentPriceHistory { get; set; } = new List<EnrollmentPriceHistory>();
    }
}
