using logic.systems.school.managment.Dto;
using logic.systems.school.managment.Models;

namespace logic.systems.school.managment.Interface
{
    public interface ITuitionService
    {
         public Task CreateByClassOfStudant (Student model, int? startManth = -99);
         
        public Task<List<Tuition>> GetByStudantId (int StudantId);

        public Task<List<Payment>> CreatePayment(CreatePaymentDTO dto);
       public Task  CreateFeePayment(CreateFeePaymentDTO dto);
        public Task<List<Payment>> GetPaymentsByStudantTuitionsId(int studentId);
        public Task<List<Models.Fines>> GetByStudantIdFinesBy(int StudantId);
        public string  GetMonthName(int monthNumber);
         
        public Task CheckFee(int? studentId);       
        public Task AutomaticRegularization(int? studentId);       
         
    }
}
