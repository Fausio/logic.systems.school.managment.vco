using logic.systems.school.managment.Dto;
using logic.systems.school.managment.Models;

namespace logic.systems.school.managment.Interface
{
    public interface ITuitionService
    {
         public Task CreateByClassOfStudant (Student model, Enrollment enrollment, string userid);
         
        public Task<List<Tuition>> GetByStudantId (int StudantId);

        public Task CreatePayment(List<CreatePaymentDTO> dto, string userid);
        public Task  CreateFeePayment(CreateFeePaymentDTO dto, string userid);
        public Task<List<TuitionPayment>> GetPaymentsByStudantTuitionsId(int studentId);
        public Task<List<Models.TuitionFine>> GetByStudantIdFinesBy(int StudantId);
        public string  GetMonthName(int monthNumber);
         
        public Task CheckFee(int? studentId, string userid);       
        public Task AutomaticRegularization(int? studentId);

        public decimal getTuitionValueByschoolLevel(string schoolLevel);


    }
}
