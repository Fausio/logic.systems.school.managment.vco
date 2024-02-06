using logic.systems.school.managment.Dto;
using logic.systems.school.managment.Models;

namespace logic.systems.school.managment.Interface
{
    public interface Idocument
    {
        public Task<AccountClosingReportDTO> GetPaymentTuitionList(DateTime? startDate, DateTime? endDate);

        public Task<EnrollmentInvoice> GetEnrollmentInvoiceByEnrollId(int EnrollId);
        public Task<List<TuitionPayment>> GetTuitionInvoiceById(int payementId); 
        public Task<List<BeneficiariesSuspededReportDTO>> GetBeneficiariesSuspeded();
    }
}
