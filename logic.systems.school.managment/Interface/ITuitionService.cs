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
        public Task<List<TuitionPaymentPageDTO>> GetPaymentsByStudantTuitionsId(int studentId);
        public Task<List<Models.TuitionFine>> GetByStudantIdFinesBy(int StudantId);
        public string  GetMonthName(int monthNumber);
         
        public Task CheckFee(int? studentId, string userid);       
        public Task AutomaticRegularization(int? studentId);
        public Task<List<TuitionPrice>> ReaTuitionPrices();
        public Task<decimal> getTuitionValueByschoolLevel(string schoolLevel, int yearId);
        public Task<TuitionPrice> ReadTuitionPriceById(int id);
        public Task<TuitionPrice> UpdateTuitionPrice(TuitionPrice entity); 
        Task RevertTuitionPayment(int id, string user);
        public Task<List<RevertTuition>> GetRevertPaymentsByStudantTuitionsId(int studantId, int enrollmentYear);

        public Task<List<YearDefinition>> ReadYearDefinitions();
        public Task generateTuitionPrice(int id);
    }
}
