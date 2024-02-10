using System.ComponentModel.DataAnnotations.Schema;

namespace logic.systems.school.managment.Models
{
    [Table("Quarter")] 
    public class Quarter : Common
    {
        public virtual Enrollment? Enrollment { get; set; }
        public int? EnrollmentId { get; set; }



        // avaliacoes o numero depende de disciplinas
        public List<Assessment> Assessments { get; set; }





    }
}
