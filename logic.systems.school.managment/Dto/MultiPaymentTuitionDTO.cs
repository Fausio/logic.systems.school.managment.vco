using DocumentFormat.OpenXml.Office2010.Excel;

namespace logic.systems.school.managment.Dto
{
    public class MultiPaymentTuitionDTO
    { 
        public int id { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public string monthName { get; set; }
        public decimal tuitionValue { get; set; }
    }
}
