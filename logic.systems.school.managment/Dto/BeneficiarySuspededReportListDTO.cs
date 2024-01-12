namespace logic.systems.school.managment.Dto
{
    public class BeneficiarySuspededReportListDTO
    {
        public DateTime EmissionDate { get; set; }
        public string StudendName { get; set; }
        public string StudentClassLevel { get; set; }

        public string MonthTuituin { get; set; }
        public decimal MonthTuituinValue { get; set; }
        public decimal PaymentTerm_first { get; set; }
        public decimal PaymentTerm_Secund { get; set; }
        public decimal TuituinPaimentStatus { get; set; }

        public decimal MonthTuituinFee { get; set; }
        public decimal MonthTuituinFeeStatus { get; set; }

    }
}
