using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace logic.systems.school.managment.Models
{
    [Table("Student")]
    public class Student : Common
    {

         public const string genderM = "Masculino";
         public const string genderF = "Feminino";

        [Required(ErrorMessage = "O campo Nome é obrigatório.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "O campo data de nascimento é obrigatório.")]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "O campo sexo é obrigatório.")]
        public string Gender { get; set; } 
        public string FatherName { get; set; }
        public string MatherName { get; set; }
        [Required(ErrorMessage = "O campo BI é obrigatório.")]
        public string PersonId { get; set; }

        // Relationship property
        public virtual SchoolLevel CurrentSchoolLevel { get; set; }
        public int CurrentSchoolLevelId { get; set; } 
        public virtual OrgUnitDistrict District { get; set; }
        [Required(ErrorMessage = "O campo distrito é obrigatório.")]
        public int DistrictId { get; set; }

        public virtual Sponsor Sponsor { get; set; }
        public int SponsorId { get; set; }


        public Student()
        {
                this.Sponsor = new Sponsor();
        }
    }
}
