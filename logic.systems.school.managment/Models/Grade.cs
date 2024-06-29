using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace logic.systems.school.managment.Models
{
    [Table("Grade")]
    public class Grade : Common
    {
        public const string GradeType_ACS = "ACS";
        public const string GradeType_AP = "AP";

        public Assessment? Assessment { get; set; }
        public int? AssessmentId { get; set; } 

        public DateTime? Date { get; set; }
        public decimal? Value { get; set; } = 0;
        public string Type { get; set; }
        public int Number { get; set; }
    }
}
