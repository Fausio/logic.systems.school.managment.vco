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
                                                    .Include(p => p.Enrollment).ThenInclude(p => p.PaymentEnrollment)
                                                    .FirstOrDefaultAsync(x => x.EnrollmentId == EnrollId);

            return result;
        }



        public async Task<PaymentTuition> GetTuitionInvoiceById(int payementId)
        {
            var result = await db.PaymentTuitions.Include(x => x.Tuition).ThenInclude(x => x.Enrollment).ThenInclude(x => x.Student)
                                                 .FirstOrDefaultAsync(x => x.Id == payementId);

            return result;
        }

        public async Task<List<PaymentTuitionListReportDTO>> GetPaymentTuitionList(DateTime? startDate, DateTime? endDate)
        {
            try
            {
                var results = new List<PaymentTuitionListReportDTO>();

                var PaymentTuitionResult = (from t in db.Tuitions
                                            join s in db.Students on t.StudentId equals s.Id
                                            join classLevel in db.SimpleEntitys on s.CurrentSchoolLevelId equals classLevel.Id
                                            join p in db.PaymentTuitions on t.Id equals p.TuitionId
                                            where t.Paid == true && p.PaymentDate >= startDate && p.PaymentDate <= endDate
                                            select new PaymentTuitionListReportDTO()
                                            {
                                                Type = "Mensalidade",
                                                StudendName = s.Name,
                                                StudentClassLevel = classLevel.Description,
                                                MonthPaid = t.MonthNumber + " - " + t.MonthName + " - " + t.Year,
                                                MonthlyFeeWithoutVat = p.PaymentWithoutVat,
                                                VatOfMonthlyFee = p.VatOfPayment,
                                                MonthlyFeeWithVat = p.PaymentWithVat

                                            }).ToList();

                if (PaymentTuitionResult != null && PaymentTuitionResult.Count > 0)
                {
                    results.AddRange(PaymentTuitionResult);
                }

                var PaymentEnrolResult = (from e in db.Enrollments
                                          join pe in db.PaymentEnrollments on e.Id equals pe.EnrollmentId
                                          join classLevel in db.SimpleEntitys on e.SchoolLevelId equals classLevel.Id
                                          where pe.Paid == true && pe.PaymentDate >= startDate && pe.PaymentDate <= endDate
                                          select new PaymentTuitionListReportDTO()
                                          {
                                              Type = "Inscrição",
                                              StudendName = e.Student.Name,
                                              StudentClassLevel = classLevel.Description,
                                              MonthPaid = "N/A",
                                              MonthlyFeeWithoutVat = pe.PaymentWithoutVat,
                                              VatOfMonthlyFee = pe.VatOfPayment,
                                              MonthlyFeeWithVat = pe.PaymentWithVat

                                          }).ToList();


                if (PaymentEnrolResult != null && PaymentEnrolResult.Count > 0)
                {
                    results.AddRange(PaymentEnrolResult);
                }


                return results;
            }
            catch (Exception x)
            {
                throw x;
            }
        }

    }
}
