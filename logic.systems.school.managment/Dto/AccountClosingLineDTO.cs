namespace logic.systems.school.managment.Dto
{
    public class AccountClosingLineDTO
    {
        public int InvoiceId { get; set; }
        public string Type { get; set; }       
        public string Student  { get; set; }

        public decimal InvoicePrice { get; set; }
        public decimal InvoiceVat { get; set; }

        public decimal InvoicePriceWithVat { get; set; }
    }
}
