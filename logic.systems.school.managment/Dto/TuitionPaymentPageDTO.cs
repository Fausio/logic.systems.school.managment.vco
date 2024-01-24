using DocumentFormat.OpenXml.Office2010.Excel;

namespace logic.systems.school.managment.Dto
{
    public class TuitionPaymentPageDTO
    {  
        public int id { get; set; }  
        public int TuitionYear { get; set; }
        public string monthName { get; set; }
        public string paymentDate { get; set; }
        public decimal paymentWithoutVat { get; set; }
        public decimal vatOfPayment { get; set; }
        public decimal paymentWithVat { get; set; }
    }
}



 