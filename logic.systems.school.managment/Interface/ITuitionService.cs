using logic.systems.school.managment.Models;

namespace logic.systems.school.managment.Interface
{
    public interface ITuitionService
    {
         public Task CreateByClassOfStudant (Student model);
         public Task<List<Tuition>> GetByStudantId (int StudantId);

        public string  GetMonthName(int monthNumber);
    }
}
