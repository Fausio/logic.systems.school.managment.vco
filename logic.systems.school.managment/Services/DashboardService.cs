using logic.systems.school.managment.Data;
using logic.systems.school.managment.Dto;
using logic.systems.school.managment.Interface;
using logic.systems.school.managment.Models;
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

        public async Task<StudentTotalsDTO> GetStudentTotals()
        {
            var result = new StudentTotalsDTO();
            result.Total = await db.Students.Where(x => x.Row != Common.Deleted).CountAsync();
            result.TotalSolved = await db.Students.Where(x => x.Row != Common.Deleted && !x.Suspended && !x.Transferred).CountAsync();
            result.TotalSuspended = await db.Students.Where(x => x.Row != Common.Deleted && x.Suspended && !x.Transferred).CountAsync();
            result.TotalTranfered = await db.Students.Where(x => x.Row != Common.Deleted && x.Transferred).CountAsync();
            return result;
        }
    }
}
