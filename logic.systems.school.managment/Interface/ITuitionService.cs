using logic.systems.school.managment.Dto;
using logic.systems.school.managment.Models;

namespace logic.systems.school.managment.Interface
{
    public interface ITuitionService
    {
         public Task CreateByClassOfStudant (Student model, int? startManth = -99);
         
        public Task<List<Tuition>> GetByStudantId (int StudantId);

        public Task<List<Payment>> CreatePayment(CreatePaymentDTO dto);
        public Task<List<Payment>> GetPaymentsByStudantTuitionsId(int studentId);
        public Task<List<Models.TuitionFines>> GetByStudantIdFinesBy(int StudantId);
        public string  GetMonthName(int monthNumber);
         
        public Task CheckFee( );
    }
}
