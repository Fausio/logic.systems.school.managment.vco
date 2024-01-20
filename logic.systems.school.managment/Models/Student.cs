using logic.systems.school.managment.Helper;
using Microsoft.EntityFrameworkCore;
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
        public bool Suspended { get; set; }

        [Required(ErrorMessage = "O campo data de nascimento é obrigatório.")]
        [UIHint("Data")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "O campo sexo é obrigatório.")]
        public string Gender { get; set; }
        public string? FatherName { get; set; }
        public string? MatherName { get; set; }
        public string? Naturalness { get; set; }
        [Required(ErrorMessage = "O campo BI é obrigatório.")]
       // [Index(IsUnique = true)]
        public string PersonId { get; set; }

        public virtual SimpleEntity CurrentSchoolLevel { get; set; }
        [Required(ErrorMessage = "O campo classe é obrigatório.")]
        public int CurrentSchoolLevelId { get; set; }
        public virtual SimpleEntity SchoolClassRoom { get; set; }
        [Required(ErrorMessage = "O campo Turma é obrigatório.")]
        public int SchoolClassRoomId { get; set; }
        public virtual OrgUnitDistrict District { get; set; }
        [Required(ErrorMessage = "O campo distrito é obrigatório.")]
        public int DistrictId { get; set; }

        public virtual Sponsor Sponsor { get; set; } = new Sponsor();
        public int SponsorId { get; set; } 
        public List<Enrollment> Enrollments { get; set; } = new List<Enrollment>();

        [NotMapped]
        public bool haveFee { get; set; }

        public bool Transferred { get; set; }
        

        public int GetAgeInDay()
        {
            if (BirthDate  != null)
            {
                DateTime now = DateTime.Now;

                // Subtrai a data de nascimento da data atual
                TimeSpan diference = now - BirthDate;

                // Calcula a idade com precisão
                int age = (int)Math.Floor(diference.TotalDays / 365.25);

                return age;
            }
            else
            {
                // Data de nascimento é nula, retorna um valor negativo indicando erro
                return -1;
            }
        }
    }
}
