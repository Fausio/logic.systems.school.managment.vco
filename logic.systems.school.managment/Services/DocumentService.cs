using DocumentFormat.OpenXml.InkML;
using logic.systems.school.managment.Data;
using logic.systems.school.managment.Dto;
using logic.systems.school.managment.Interface;
using logic.systems.school.managment.Models;
using Microsoft.EntityFrameworkCore;

namespace logic.systems.school.managment.Services
{
    public class DocumentService : Idocument
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext(new DbContextOptions<ApplicationDbContext>());

        public async Task<EnrollmentInvoice> GetEnrollmentInvoiceByEnrollId(int EnrollId)
        {
            var result = await db.EnrollmentInvoices.Include(x => x.Enrollment).ThenInclude(x => x.Student)
                                                    .Include(p => p.Enrollment).ThenInclude(p=> p.PaymentEnrollment)
                                                    .FirstOrDefaultAsync(x => x.EnrollmentId == EnrollId);

            return result;
        }

        public async Task<List<PaymentTuitionListReportDTO>> GetPaymentTuitionList(DateTime? startDate, DateTime? endDate)
        {
            try
            {
                  var result = from t in db.Tuitions
                             join s in db.Students on t.StudentId equals s.Id
                             join classLevel in db.SimpleEntitys on s.CurrentSchoolLevelId equals classLevel.Id
                             join p in db.PaymentTuitions on t.Id equals p.TuitionId
                             where t.Paid == true && p.PaymentDate >= startDate && p.PaymentDate <= endDate
                             select new PaymentTuitionListReportDTO()
                             {
                                 StudendName = s.Name,
                                 StudentClassLevel = classLevel.Description,
                                 MonthPaid = t.MonthNumber + " - " + t.MonthName + " - " + t.Year,
                                 MonthlyFeeWithoutVat = p.PaymentWithoutVat,
                                 VatOfMonthlyFee = p.VatOfPayment,
                                 MonthlyFeeWithVat = p.PaymentWithVat
                             };

             return   result.ToList(); 
            }
            catch (Exception x)
            {

                throw x;
            }
        }
    }
}
