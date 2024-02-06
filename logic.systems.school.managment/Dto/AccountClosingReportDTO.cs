namespace logic.systems.school.managment.Dto
{
    public class AccountClosingReportDTO
    {
        public string Site { get; set; }
        public DateTime EmissionDate { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public List<AccountClosingLineDTO> InvoiceLine { get; set; } = new List<AccountClosingLineDTO>();

        public decimal TotalInvoicePrice => InvoiceLine.Sum(x => x.InvoicePrice);
        public decimal TotalInvoiceVat => InvoiceLine.Sum(x => x.InvoiceVat);
        public decimal TotalInvoicePriceWithVat => InvoiceLine.Sum(x => x.InvoicePriceWithVat);
    }
}
