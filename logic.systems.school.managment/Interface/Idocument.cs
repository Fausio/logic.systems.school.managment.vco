using logic.systems.school.managment.Dto;

namespace logic.systems.school.managment.Interface
{
    public interface Idocument
    {
        public Task<List<PaymentTuitionListReportDTO>> GetPaymentTuitionList(DateTime? startDate, DateTime? endDate);
    }
}
