using logic.systems.school.managment.Data;
using logic.systems.school.managment.Dto;
using logic.systems.school.managment.Interface;
using Microsoft.EntityFrameworkCore;

namespace logic.systems.school.managment.Services
{
    public class DashboardService : IDashBoard
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext(new DbContextOptions<ApplicationDbContext>());

        public async Task<List<CanvarChartDTO>> GetAllMonthsFeeByCurrentYear()
        {
            var result = from tf in db.TuitionFines
                         join t in  db.Tuitions on tf.TuitionId equals t.Id
                         where t.Year == DateTime.Now.Year
                         group tf by new { t.MonthName,t.MonthNumber } into g
                         orderby g.Key.MonthNumber
                         select new CanvarChartDTO()
                         {
                             label = g.Key.MonthName,
                             y = (int)g.Sum(tf => tf.FinesValue)
                         };

            return  await result.ToListAsync();
        }
    }
}
