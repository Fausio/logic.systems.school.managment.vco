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
                    Site = "ESCOLA PRIMÁRIA E SECUNDÁRIA PANDA ALEGRIA NACALA-PORTO",

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
                            var _MonthTuitionValue = (getTuitionValueByschoolLevel(associatedLeve.Description) - discount); ;

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


        private decimal getTuitionValueByschoolLevel(string schoolLevel)
        {

            #region a logica da KALIMANY
            //3500-- Pré - escola A
            //3500-- Pré - escola B
            //3500-- Pré - escola C
            //-------------------- -
            //4000-- 1ª classe
            //---------------------
            //3700-- 2ª classe
            //3700-- 3ª classe
            //3700-- 4ª classe
            //3700-- 5ª classe
            //3700-- 6ª classe
            //3700-- 7ª classe
            //---------------------
            //3800-- 8ª classe
            //3800-- 9ª classe
            //3800-- 10ª classe
            //---------------------
            //4200-- 11ª classe
            //4200-- 12ª classe
            #endregion


            var Price_3500 = new List<string>()
            {
                "Pré-escola"
            };
            var Price_4000 = new List<string>() { "1ª classe" };
            var Price_3700 = new List<string>()
            {
                "2ª classe"     ,
                "3ª classe"     ,
                "4ª classe"     ,
                "5ª classe"     ,
                "6ª classe"     ,
                "7ª classe"
            };
            var Price_3800 = new List<string>()
            {
                "8ª classe"      ,
                "9ª classe"     ,
                "10ª classe"

            };
            var Price_4200 = new List<string>()
            {
                "11ª classe"    ,
                "12ª classe"
            };

            if (Price_3500.Contains(schoolLevel)) { return 3500; }
            else if (Price_4000.Contains(schoolLevel)) { return 4000; }
            else if (Price_3700.Contains(schoolLevel)) { return 3700; }
            else if (Price_3800.Contains(schoolLevel)) { return 3800; }
            else if (Price_4200.Contains(schoolLevel)) { return 4200; }
            else
            {
                return 0;
            }
        }



    }
}
