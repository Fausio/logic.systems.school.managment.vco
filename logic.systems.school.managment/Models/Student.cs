using logic.systems.school.managment.Helper;
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




        public int AgeInMonthsMethod(DateTime? lastDate)
        {
            var months = (lastDate.Value.Year - BirthDate.Year) * 12;
            months -= BirthDate.Month + 1;
            months += lastDate.Value.Month;
            return months <= 0 ? 0 : months;
        }
        public int? AgeInYears { get { return AgeInYearsMethod(null); } }
        public int AgeInYearsMethod(DateTime? date)
        {
            DateTime birthDate = BirthDate;
            DateTime today = DateTime.Today;

            if (date != null) { today = date.Value; }

            int age = today.Year - birthDate.Year;

            if (birthDate.Date > today.AddYears(-age))
            {
                age--;
            }

            return age;
        }
    }
}
