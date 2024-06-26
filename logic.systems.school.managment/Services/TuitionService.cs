﻿using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Spreadsheet;
using Humanizer;
using logic.systems.school.managment.Data;
using logic.systems.school.managment.Dto;
using logic.systems.school.managment.Interface;
using logic.systems.school.managment.Migrations;
using logic.systems.school.managment.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Drawing;
using System.Globalization;

namespace logic.systems.school.managment.Services
{
    public class TuitionService : ITuitionService
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext(new DbContextOptions<ApplicationDbContext>());
        List<String> classesWithoutExame = new List<String>()
            {
                "Pré-escola"  ,
                "1ª classe"   ,
                "2ª classe"   ,
                "3ª classe"   ,
                "4ª classe"   ,
                "5ª classe"   ,
                "6ª classe"   ,
                "7ª classe"   ,
                "8ª classe"   ,
                "9ª classe"   ,
                "10ª classe"
            };

        List<String> classesWithtExame = new List<String>()
            {
              "11ª classe"  ,
              "12ª classe"

            };
        public string GetMonthName(int month)
        {
            CultureInfo ptCulture = new CultureInfo("pt-BR");
            DateTimeFormatInfo dtfi = DateTimeFormatInfo.GetInstance(ptCulture);
            return dtfi.GetMonthName(month);
        }




        public async Task CreateByClassOfStudant(Student model, Enrollment enrollment, string userid)
        {


            // meses de estudo (1 de fevereiro a  dezembro) para decima e decima-segunda classe, devido aos exame.
            // meses  de estudo de (1 de fevereiro a novembro) são todas as outras classes sem exame incluido sexta classe que tem exame.


            if (classesWithtExame.Contains(model.CurrentSchoolLevel.Description))
            {
                List<Tuition> tuitions = new List<Tuition>();
                string[] meses = { "Fevereiro", "Março", "Abril", "Maio", "Junho", "Julho", "Agosto", "Setembro", "Outubro", "Novembro", "Dezembro" };
                for (int i = 1; i < meses.Length + 1; i++)
                {

                    int daysInMonth = DateTime.DaysInMonth(enrollment.EnrollmentYear, i + 1);
                    DateTime startDate = new DateTime(enrollment.EnrollmentYear, i + 1, 1);
                    DateTime endDate = new DateTime(enrollment.EnrollmentYear, i + 1, daysInMonth);

                    tuitions.Add(new Tuition()
                    {
                        MonthNumber = i,
                        MonthName = meses[i - 1],
                        StartDate = startDate,
                        EndDate = endDate,
                        Year = enrollment.EnrollmentYear,
                        StudentId = model.Id,
                        AssociatedLevelId = model.CurrentSchoolLevelId,
                        EnrollmentId = enrollment.Id,
                        CreatedUSer = userid
                    });

                }

                await db.Tuitions.AddRangeAsync(tuitions);
                await db.SaveChangesAsync();
            }
            if (this.classesWithoutExame.Contains(model.CurrentSchoolLevel.Description))
            {
                List<Tuition> tuitions = new List<Tuition>();
                string[] meses = { "Fevereiro", "Março", "Abril", "Maio", "Junho", "Julho", "Agosto", "Setembro", "Outubro", "Novembro" };
                for (int i = 1; i < meses.Length + 1; i++)
                {

                    int daysInMonth = DateTime.DaysInMonth(enrollment.EnrollmentYear, i + 1);
                    DateTime startDate = new DateTime(enrollment.EnrollmentYear, i + 1, 1);
                    DateTime endDate = new DateTime(enrollment.EnrollmentYear, i + 1, daysInMonth);

                    tuitions.Add(new Tuition()
                    {
                        MonthNumber = i,
                        MonthName = meses[i - 1],
                        StartDate = startDate,
                        EndDate = endDate,
                        Year = enrollment.EnrollmentYear,
                        StudentId = model.Id,
                        AssociatedLevelId = model.CurrentSchoolLevelId,
                        EnrollmentId = enrollment.Id
                    });

                }

                await db.Tuitions.AddRangeAsync(tuitions);
                await db.SaveChangesAsync();
            }

        }

        public async Task<List<Tuition>> GetByStudantId(int StudantId)
        {
            try
            {
                return await db.Tuitions.Include(e => e.Enrollment.SchoolLevel).Where(x => x.StudentId == StudantId).ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<Models.TuitionFine>> GetByStudantIdFinesBy(int StudantId)
        {
            try
            {
                var tuitions = await GetByStudantId(StudantId);
                var tuitionsFines = new List<Models.TuitionFine>();
                if (tuitions.Count > 0)
                {
                    foreach (var item in tuitions)
                    {
                        var finded = await db.TuitionFines.Include(x => x.TuitionFineDailies)
                                                          .FirstOrDefaultAsync(x => x.TuitionId == item.Id);



                        if (finded is not null)
                        {
                            if (finded.TuitionFineDailies.Count > 0)
                            {
                                finded.FinesValue = (finded.FinesValue + finded.TuitionFineDailies.Sum(x => x.FinesValue));

                                finded.TuitionFineDailies = null;
                            }


                            tuitionsFines.Add(finded);
                        }

                    }
                }

                return tuitionsFines;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<TuitionPaymentPageDTO>> GetPaymentsByStudantTuitionsId(int studentId)
        {
            try
            {
                var student = await db.Students.Include(x => x.Enrollments).ThenInclude(x => x.Tuitions).FirstOrDefaultAsync(x => x.Id == studentId && x.Row != Common.Deleted);
                var listOfPaymentsClass = new List<TuitionPayment>();
                var tuitions = student.Enrollments.SelectMany(x => x.Tuitions).Select(x => x.Id).ToList();
                foreach (int item in tuitions)
                {
                    var payment = await db.PaymentTuitions.Include(x => x.Tuition).FirstOrDefaultAsync(x => x.TuitionId == item);

                    if (payment is not null)
                    {
                        listOfPaymentsClass.Add(payment);
                    }

                }


                var listOfPayments = new List<TuitionPaymentPageDTO>();

                var groupedByDate = listOfPaymentsClass.GroupBy(payment => payment.CreatedDate);

                List<TuitionPaymentPageDTO> result = groupedByDate.Select((group) =>
                    new TuitionPaymentPageDTO
                    {
                        id = group.First().Id, // Pode ser qualquer ID, já que você está agrupando por data 
                        TuitionYear = group.First().Tuition.Year,
                        monthName = string.Join(", ", group.Select(p => p.Tuition.MonthName).Distinct()),
                        paymentDate = group.First().PaymentDate.ToString("dd/MM/yyyy"),
                        paymentWithoutVat = group.Sum(payment => payment.PaymentWithoutVat),
                        vatOfPayment = group.Sum(payment => payment.VatOfPayment),
                        paymentWithVat = group.Sum(payment => payment.PaymentWithVat),


                    }).ToList();

                return result;
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        public async Task CreateFeePayment(CreateFeePaymentDTO dto, string userid)
        {
            try
            {
                var TuitionFines = await db.TuitionFines.FirstOrDefaultAsync(x => x.Id == dto.TuitionFeeId);

                if (TuitionFines is not null && TuitionFines.Id > 0)
                {

                    TuitionFines.Paid = true;
                    TuitionFines.PaidDate = DateTime.Now;
                    TuitionFines.UpdatedDate = DateTime.Now;
                    TuitionFines.Row = Common.Modified;
                    TuitionFines.UpdatedUSer = userid;
                    db.TuitionFines.Update(TuitionFines);
                    await db.SaveChangesAsync();
                }


            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task CreatePayment(List<CreatePaymentDTO> dtos, string userid)
        {

            try
            {
                var nowTimeStep = DateTime.UtcNow;

                foreach (var dto in dtos)
                {
                    var TuitionPrice = await db.Tuitions.Include(x => x.Enrollment).FirstOrDefaultAsync(x => x.Id == dto.TuitionId);

                    var studant = await db.Students.Include(x => x.Enrollments)
                                             .ThenInclude(x => x.Tuitions)
                                             .Include(x => x.CurrentSchoolLevel)
                                             .FirstOrDefaultAsync(x => x.Id == dto.StudantId && x.Row != Common.Deleted);

                    if (dto.StudantId > 0 && dto.TuitionId > 0 && studant is not null)
                    {
                        var payment = new TuitionPayment()
                        {
                            TuitionId = dto.TuitionId,
                            PaymentDate = DateTime.Now,
                            CreatedDate = nowTimeStep,
                            CreatedUSer = userid,
                        };

                        payment.PaymentWithVat = TuitionPrice.Enrollment.TuitionPrice;
                        payment.VatOfPayment = VatCalc(TuitionPrice.Enrollment.TuitionPrice);
                        payment.PaymentWithoutVat = payment.PaymentWithVat - payment.VatOfPayment;

                        var invoice = new TuitionInvoice()
                        {
                            Date = payment.PaymentDate,
                            CreatedDate = nowTimeStep,
                            CreatedUSer = userid,
                            Invoice = new Invoice() { }
                        };

                        await db.TuitionInvoices.AddAsync(invoice);
                        await db.SaveChangesAsync();

                        studant.UpdatedDate = DateTime.Now;
                        studant.Row = Common.Modified;
                        studant.UpdatedUSer = userid;

                        payment.TuitionInvoice = invoice;
                        await db.PaymentTuitions.AddAsync(payment);
                        await db.SaveChangesAsync();

                        var tuitionPayed = db.Tuitions.Include(x => x.TuitionFines).ThenInclude(x => x.TuitionFineDailies).FirstOrDefault(x => x.Id == dto.TuitionId);

                        if (tuitionPayed != null)
                        {
                            tuitionPayed.Row = Common.Modified;
                            tuitionPayed.UpdatedDate = DateTime.Now;
                            tuitionPayed.PaidDate = DateTime.Now;
                            tuitionPayed.Paid = true;
                            tuitionPayed.UpdatedUSer = userid;

                            db.Tuitions.Update(tuitionPayed);
                            await db.SaveChangesAsync();

                            // create Fee Payment if Tuition have it  
                            if (tuitionPayed.TuitionFines is not null)
                            {

                                var DailyFees = (decimal)0;

                                if (tuitionPayed.TuitionFines.TuitionFineDailies.Count > 0)
                                {
                                    DailyFees = tuitionPayed.TuitionFines.TuitionFineDailies.Sum(x => x.FinesValue);

                                    foreach (var item in tuitionPayed.TuitionFines.TuitionFineDailies)
                                    {
                                        item.UpdatedUSer = userid;
                                        item.Row = Common.Modified;
                                        item.PaidDate = payment.PaymentDate;
                                        item.Paid = true;
                                    }
                                }


                                tuitionPayed.TuitionFines.FinesValue += DailyFees;
                                tuitionPayed.TuitionFines.Paid = true;
                                tuitionPayed.TuitionFines.PaidDate = payment.PaymentDate;
                                tuitionPayed.UpdatedUSer = userid;
                                tuitionPayed.Row = Common.Modified;

                                db.TuitionFines.Update(tuitionPayed.TuitionFines);
                                await db.SaveChangesAsync();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private decimal VatCalc(decimal valor, decimal taxaIVA = 0.05m)
        {
            // Verifica se a taxa IVA fornecida é válida (0% a 100%)
            if (taxaIVA < 0 || taxaIVA > 1)
            {
                throw new ArgumentException("A taxa de IVA deve estar entre 0% e 100%.");
            }

            // Calcula o valor do IVA
            decimal iva = valor * taxaIVA;

            return iva;
        }


        public decimal getTuitionValueByschoolLevel(string schoolLevel)
        {


            var Price_4095 = new List<string>()
            {   "1ª classe"     ,
                "2ª classe"     ,
                "3ª classe"     ,
                "4ª classe"     ,
                "5ª classe"     ,
                "6ª classe"
            };
            var Price_4305 = new List<string>()
            {   "7ª classe"     ,
                "8ª classe"     ,
                "9ª classe"     ,
                "10ª classe"     ,
                "11ª classe"     ,
                "12ª classe"
            };

            if (Price_4095.Contains(schoolLevel)) { return 4095; }
            else if (Price_4305.Contains(schoolLevel)) { return 4305; }

            else
            {
                return 0;
            }
        }


        private async Task CheckFeeZiro()
        {
            var payments = await db.PaymentTuitions.Include(x => x.Tuition)
                                                    .ThenInclude(x => x.Enrollment)
                                                   .Where(x => x.PaymentWithoutVat == (decimal)0)
                                                   .ToListAsync();

            foreach (var item in payments)
            {
                var studant = await db.Students.Include(x => x.Enrollments)
                                           .ThenInclude(x => x.Tuitions)
                                           .Include(x => x.CurrentSchoolLevel)
                                           .FirstOrDefaultAsync(x => x.Id == item.Tuition.StudentId && x.Row != Common.Deleted);
                 
                if (studant is not null)
                {
                    item.PaymentWithVat = item.Tuition.Enrollment.TuitionPrice;
                    item.VatOfPayment = VatCalc(item.Tuition.Enrollment.TuitionPrice);
                    item.PaymentWithoutVat = item.PaymentWithVat - item.VatOfPayment; 

                    db.PaymentTuitions.Update(item);
                    await db.SaveChangesAsync();
                }


            }

        }



        public async Task CheckFee(int? studantId, string userid)
        {

            await CheckFeeZiro();
            var students = new List<Student>();

            if (studantId is not null && studantId > 0)
            {
                students = await db.Students.Include(x => x.CurrentSchoolLevel).Include(x => x.Enrollments).ThenInclude(x => x.Tuitions).Where(x => x.Row != Common.Deleted && x.Id == studantId).ToListAsync();

            }

            if (studantId is not null && studantId == 0)
            {
                students = await db.Students.Include(x => x.CurrentSchoolLevel).Include(x => x.Enrollments).ThenInclude(x => x.Tuitions).Where(x => x.Row != Common.Deleted).ToListAsync();
            }

            var now = DateTime.Now;
            foreach (Student student in students)
            {

                if (!student.Transferred)
                {
                    var Tuitions = student.Enrollments.SelectMany(x => x.Tuitions.Where(t => !t.Paid));
                    var setSuspended = false;
                    foreach (Tuition tuition in Tuitions)
                    {
                        var tuituionStartDate_part_1 = tuition.StartDate.AddDays(24);

                        var tuituionStartDate_part_2 = new DateTime(tuition.StartDate.Year, tuition.StartDate.AddMonths(1).Month, 5);
                        // recebe multa de 20% se nao pagar mensalidade entre dia 25 e 5 do outro mês.

                        if (now >= tuituionStartDate_part_1)
                        {
                            await CreateTuitionFine(tuition.Id, userid);

                        }
                    }

                    if (setSuspended != student.Suspended)
                    {

                        student.UpdatedUSer = userid;
                        student.Suspended = setSuspended;
                        await db.SaveChangesAsync();
                    }
                }


            }
        }

        private async Task CreateTuitionFine(int tuitionId, string userid)
        {

            var havetuitionFines = await db.TuitionFines.FirstOrDefaultAsync(x => x.TuitionId == tuitionId);
            // para nao duplicar multa principal de 300
            if (havetuitionFines == null)
            {
                var tuition = await db.Tuitions.Include(x => x.Enrollment).ThenInclude(x => x.SchoolLevel).FirstOrDefaultAsync(x => x.Id == tuitionId && !x.Paid);

                if (tuition is not null)
                {
                    var tuitionFines = new TuitionFine()
                    {
                        TuitionId = tuitionId,
                        CreatedUSer = userid,
                        FinesValue = tuition.Enrollment.TuitionPrice * 0.2m,

                    };
                    await db.TuitionFines.AddAsync(tuitionFines);
                    await db.SaveChangesAsync();
                }
            }


        }

        public async Task AutomaticRegularization(int? studentId)
        {

            if (studentId is not null || studentId > 0)
            {
                await singleAutomaticRegularization(studentId);
            }
            else
            {
                var students = await db.Students.Where(x => x.Row != Common.Deleted).ToListAsync();

                // methorar pra nao usar foreach
                foreach (var item in students)
                {
                    await singleAutomaticRegularization(item.Id);
                }
            }

        }

        private async Task singleAutomaticRegularization(int? studentId)
        {

            var Tuitions = await db.Tuitions.AnyAsync(x => x.StudentId == studentId && !x.Paid);

            var TuitionsFee = await db.Tuitions.AnyAsync(x => x.StudentId == studentId && !x.TuitionFines.Paid);

            if (!Tuitions && !TuitionsFee)
            {
                var student = await db.Students.FirstOrDefaultAsync(x => x.Id == studentId && x.Row != Common.Deleted);
                student.Suspended = false;
                await db.SaveChangesAsync();
            }
        }
    }
}

//as mensalidades devem ser antecipadamente pagas no inicio de cada Mes, do dia 1 a dia15, apartir do dia 16 a 25 a multa é de 300

//depois do dia 25 o aluno passa a ser suspenso






