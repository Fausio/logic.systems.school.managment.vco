using logic.systems.school.managment.Dto;

namespace logic.systems.school.managment.Interface
{
    public interface IDashBoard
    {
        public Task<List<CanvarChartDTO>> GetAllMonthsFeeByCurrentYear();
    }
}
