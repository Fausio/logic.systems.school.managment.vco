using Microsoft.VisualBasic;

namespace logic.systems.school.managment.Dto
{
    public class PaymentTuitionListReportDTO
    {

        public DateTime  EmissionDate { get; set;}
        public string BoxNumber{ get; set;}

        public DateTime StartDate { get; set;}
        public DateTime EndDate { get; set;}

        public string StudendName { get; set; }
        public string StudentClassLevel { get; set; }

        public string MonthPaid { get; set; }

        public decimal TuitionPayd { get; set; } 

        public decimal total { get; set; }

    }
}
