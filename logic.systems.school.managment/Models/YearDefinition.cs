using System.ComponentModel.DataAnnotations.Schema;

namespace logic.systems.school.managment.Models
{
    [Table("YearDefinition")]
    public class YearDefinition : Common
    {
        public int Year { get; set; }
        public List<TuitionPrice> TuitionPrices { get; set; } = new List<TuitionPrice>();
        public List<EnrollmentPrice> EnrollmentPrices { get; set; } = new List<EnrollmentPrice>();
    }
}
