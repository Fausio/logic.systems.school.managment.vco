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
    
        public const string DiscountWithout = "Sem desconto"; 
        public const string DiscountPersonInCharge = "Encarregado 100,00 MT"; 
        public const string DiscountTeacher = "Professor 500,00 MT";

         

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
        public bool Internal { get; set; }

        
        public string DiscountType { get; set; }


        public int GetAgeInDay()
        {
            if (BirthDate != null)
            {
                DateTime now = DateTime.Now; 
                TimeSpan diference = now - BirthDate; 
                int age = (int)Math.Floor(diference.TotalDays / 365.25);

                return age;
            }
            else
            { 
                return -1;
            }
        }
    }
}
