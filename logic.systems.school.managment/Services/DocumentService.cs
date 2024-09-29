using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.InkML;
using DocumentFormat.OpenXml.Spreadsheet;
using logic.systems.school.managment.Data;
using logic.systems.school.managment.Dto;
using logic.systems.school.managment.Interface;
using logic.systems.school.managment.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace logic.systems.school.managment.Services
{
    public class DocumentService : Idocument
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext(new DbContextOptions<ApplicationDbContext>());

        public async Task<EnrollmentInvoice> GetEnrollmentInvoiceByEnrollId(int EnrollId)
        {
            var result = await db.EnrollmentInvoices.Include(x => x.Enrollment).ThenInclude(x => x.EnrollmentItems)
                                                    .Include(x => x.Enrollment).ThenInclude(x => x.Student)
                                                    .Include(p => p.Enrollment).ThenInclude(p => p.PaymentEnrollment)
                                                    .FirstOrDefaultAsync(x => x.EnrollmentId == EnrollId);

            return result;
        }



        public async Task<List<TuitionPayment>> GetTuitionInvoiceById(int payementId)
        {


            var result = await db.PaymentTuitions.Include(x => x.Tuition)
                                                 .ThenInclude(x => x.Enrollment).ThenInclude(x => x.Student)
                                                 .Include(x => x.Tuition.TuitionFines)
                                                 .ThenInclude(x => x.TuitionFineDailies)
                                                 .FirstOrDefaultAsync(x => x.Id == payementId);

            var student = await db.Students.FirstOrDefaultAsync(x => x.Id == result.Tuition.StudentId);

            var listOfResult = await db.PaymentTuitions.Include(x => x.Tuition)
                                                       .ThenInclude(x => x.Enrollment)
                                                       .ThenInclude(x => x.Student)
                                                       .Include(x => x.Tuition.TuitionFines)
                                                       .Where(x => x.CreatedDate == result.CreatedDate && x.Tuition.StudentId == student.Id)
                                                       .ToListAsync();
            return listOfResult;
        }

        public async Task<AccountClosingReportDTO> GetPaymentTuitionList(DateTime? startDate, DateTime? endDate)
        {
            try
            {



                var results = new AccountClosingReportDTO()
                {

                    EmissionDate = DateTime.Now,
                    EndDate = endDate.Value,
                    StartDate = startDate.Value,
                    Site = "COOPERATIVA DE ENSINO KALIMANY",

                };


                var x = await db.Tuitions.Include(x => x.TuitionFines)
                                         .ThenInclude(d => d.TuitionFineDailies)
                                         .Where(x => x.Paid)
                                         .ToListAsync();




                var PaymentTuitionResult = (from t in db.Tuitions.Include(x => x.TuitionFines).ThenInclude(d => d.TuitionFineDailies)
                                            join p in db.PaymentTuitions on t.Id equals p.TuitionId
                                            where t.Paid == true && p.PaymentDate >= startDate && p.PaymentDate <= endDate
                                            select new AccountClosingLineDTO()
                                            {
                                                InvoiceId = t.Id,
                                                Type = "Mensalidade",
                                                Student = t.Enrollment.Student.Name,
                                                InvoicePrice = p.PaymentWithoutVat +
                                                                                                       (t.TuitionFines != null ? t.TuitionFines.FinesValue : 0) +
                                                                                                       (t.TuitionFines != null && t.TuitionFines.TuitionFineDailies != null
                                                                                                            ? t.TuitionFines.TuitionFineDailies.Sum(x => x.FinesValue)
                                                                                                            : 0),
                                                InvoiceVat = p.VatOfPayment,
                                                InvoicePriceWithVat = p.PaymentWithVat +
                                                                                                              (t.TuitionFines != null ? t.TuitionFines.FinesValue : 0) +
                                                                                                              (t.TuitionFines != null && t.TuitionFines.TuitionFineDailies != null
                                                                                                                   ? t.TuitionFines.TuitionFineDailies.Sum(x => x.FinesValue)
                                                                                                                   : 0),
                                            }).ToList();


                if (PaymentTuitionResult != null && PaymentTuitionResult.Count > 0)
                {
                    results.InvoiceLine.AddRange(PaymentTuitionResult);
                }

                var PaymentEnrolResult = (from e in db.Enrollments.Include(x => x.EnrollmentItems).Include(x => x.PaymentEnrollment)
                                          where e.PaymentEnrollment != null && e.PaymentEnrollment.Paid == true &&
                                                e.PaymentEnrollment.PaymentDate >= startDate && e.PaymentEnrollment.PaymentDate <= endDate
                                          select new AccountClosingLineDTO()
                                          {
                                              InvoiceId = e.Id,
                                              Type = "Inscrição",
                                              Student =e.Student.Name,
                                              InvoicePrice = e.PaymentEnrollment.PaymentWithoutVat +
                                                             (e.EnrollmentItems != null ? e.EnrollmentItems.Sum(x => x.Price) : 0),
                                              InvoiceVat = e.PaymentEnrollment.VatOfPayment,
                                              InvoicePriceWithVat = e.PaymentEnrollment.PaymentWithoutVat +
                                                                    (e.EnrollmentItems != null ? e.EnrollmentItems.Sum(x => x.Price) : 0),
                                          }).ToList();



                if (PaymentEnrolResult != null && PaymentEnrolResult.Count > 0)
                {
                    results.InvoiceLine.AddRange(PaymentEnrolResult);
                }


                return results;
            }
            catch (Exception x)
            {
                throw x;
            }
        }

        public async Task<List<BeneficiariesSuspededReportDTO>> GetBeneficiariesSuspeded()
        {
            try
            {
                var enrollments = await db.Enrollments.Select(x => x.Student).Where(x => x.Suspended).ToListAsync();
                var results = new List<BeneficiariesSuspededReportDTO>();

                if (enrollments is not null)
                {
                    foreach (var s in enrollments)
                    {
                        var currentSchoolLevel = await db.SimpleEntitys.FirstOrDefaultAsync(x => x.Id == s.CurrentSchoolLevelId);

                        results.Add(new BeneficiariesSuspededReportDTO()
                        {
                            StudendId = s.Id,
                            StudendName = s.Name,
                            StudendGender = s.Gender,
                            StudendBirthDate = s.BirthDate.ToString("dd/MM/yyyy"),
                            StudentClassLevel = currentSchoolLevel.Description
                        });
                    }

                    foreach (var item in results)
                    {
                        var suspendedInfo = new List<BeneficiariesSuspededReportItemDTO>();

                        // Obtém a data atual
                        var now = DateTime.Now;

                        // Volta para o primeiro dia do mês
                        //var currentMonthLastFeeDay = new DateTime(now.Year, now.Month, 25);


                        //now.AddMonths(24);
                        var Tuituins = await db.Tuitions
                                               .Include(x => x.TuitionFines)
                                               .ThenInclude(x => x.TuitionFineDailies)
                                               .Include(x => x.Enrollment)
                                               .ThenInclude(x => x.Student)
                                               .Where(x => x.StudentId == item.StudendId
                                                        && !x.Paid
                                                       && x.TuitionFines != null
                                                      )
                                                .ToArrayAsync();



                        foreach (var t in Tuituins)
                        {
                            var associatedLeve = await db.SimpleEntitys.FirstOrDefaultAsync(x => x.Id == t.AssociatedLevelId);


                            var TotalFeeDaily = (decimal)0;

                            if (t.TuitionFines != null)
                            {
                                if (t.TuitionFines.TuitionFineDailies != null || t.TuitionFines.TuitionFineDailies.Count() > 0)
                                {
                                    TotalFeeDaily = t.TuitionFines.TuitionFineDailies.Sum(x => x.FinesValue);
                                }

                            }

                            var discount = (decimal)0;
                            if (t.Enrollment.Student is not null)
                            {

                                if (t.Enrollment.Student.DiscountType == Student.DiscountPersonInCharge)
                                {
                                    discount = 100;
                                }
                                else if (t.Enrollment.Student.DiscountType == Student.DiscountTeacher)
                                {
                                    discount = 500;
                                }
                            }

                            var tuitionValue = await db.TuitionPrices.FirstOrDefaultAsync(x => x.Description == associatedLeve.Description);

                            var _MonthTuitionValue = (tuitionValue.Price - discount); ;

                            suspendedInfo.Add(new BeneficiariesSuspededReportItemDTO()
                            {
                                MonthTuition = t.MonthName + " - " + t.Year,
                                AssociatedLevel = associatedLeve.Description,
                                MonthTuitionValue = _MonthTuitionValue,
                                PaymentTerm_first = t.StartDate.AddDays(15).ToString("dd/MM/yyyy"),
                                PaymentTerm_Secund = t.StartDate.AddDays(24).ToString("dd/MM/yyyy"),
                                TuitionPaimentStatus = t.Paid ? "Pago" : "Não pago",
                                MonthTuitionFee = (300 + TotalFeeDaily)
                            });
                        }

                        item.items = suspendedInfo;
                    }

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
