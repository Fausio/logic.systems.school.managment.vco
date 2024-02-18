using logic.systems.school.managment.Models;
using System.ComponentModel.DataAnnotations;

namespace logic.systems.school.managment.Dto
{
    public class CreateStudantDTO : AuditDTO
    {
        [Required(ErrorMessage = "O campo Nome é obrigatório.")]
        public string Name { get; set; }

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

        [Required(ErrorMessage = "O campo distrito é obrigatório.")]
        public int DistrictId { get; set; }


        [Required(ErrorMessage = "O campo classe é obrigatório.")]
        public int CurrentSchoolLevelId { get; set; }
        [Required(ErrorMessage = "O campo Turma é obrigatório.")]
        public int SchoolClassRoomId { get; set; }

        [Required(ErrorMessage = "O campo Ano de inscrição  é obrigatório.")]
        public int EnrollmentYear { get; set; } 
        public decimal TuitionPrice { get; set; }
        #region  Sponsor
        [Required(ErrorMessage = "O campo Nome do encarregado de Educação é obrigatório.")]
        public string SponsorName { get; set; }
        public string? SponsorAddress { get; set; }
        public string? SponsorEducation { get; set; }

        public string[] SponsorContacts { get; set; } = new string[] { "" };


        #endregion

        public bool EnroolAllMonths { get; set; }
        public bool Internal { get; set; }
        public string DiscountType { get; set; }

        public CreateStudantDTO()
        {
            Internal = true;
            BirthDate = DateTime.Now.AddYears(-21);
            DiscountType = Student.DiscountWithout;
        }

    }
}
