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

        #region  Sponsor
        [Required(ErrorMessage = "O campo Nome do encarregado de Educação é obrigatório.")]
        public string SponsorName { get; set; }
        public string? SponsorAddress { get; set; }
        public string? SponsorEducation { get; set; }

        public string[] SponsorContacts { get; set; } = new string[] { "" };


        #endregion

        public bool EnroolAllMonths { get; set; }

        //public int StartTuition { get; set; }

        //public List<Tuition>? Tuitions { get; set; } = new List<Tuition>();

        public CreateStudantDTO()
        {
            this.BirthDate = DateTime.Now.AddYears(-21);
            //string[] meses = { "Fevereiro", "Março", "Abril", "Maio", "Junho", "Julho", "Agosto", "Setembro", "Outubro", "Novembro", "Dezembro" };
            //for (int i = 1; i < meses.Length + 1; i++)
            //{
            //    int daysInMonth = DateTime.DaysInMonth(DateTime.Now.Year, i + 1);
            //    DateTime startDate = new DateTime(DateTime.Now.Year, i + 1, 1);
            //    DateTime endDate = new DateTime(DateTime.Now.Year, i + 1, daysInMonth);

            //    Tuitions.Add(new Tuition()
            //    {
            //        MonthNumber = i,
            //        MonthName = meses[i - 1],
            //        StartDate = startDate,
            //        EndDate = endDate,
            //        Year = DateTime.Now.Year,
            //        Enrollment = new Enrollment()
            //    });
            //}
        }

    }
}
