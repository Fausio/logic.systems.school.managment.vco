using logic.systems.school.managment.Data;
using logic.systems.school.managment.Dto;
using logic.systems.school.managment.Interface;
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
                "Pré-escola A",
                "Pré-escola B",
                "Pré-escola C",
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




        public async Task CreateByClassOfStudant(Student model, Enrollment enrollment)
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
                        EnrollmentId = enrollment.Id
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
                return await db.Tuitions.Where(x => x.StudentId == StudantId).ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<Models.Fines>> GetByStudantIdFinesBy(int StudantId)
        {
            try
            {
                var tuitions = await GetByStudantId(StudantId);
                var tuitionsFines = new List<Models.Fines>();
                if (tuitions.Count > 0)
                {
                    foreach (var item in tuitions)
                    {
                        var finded = await db.TuitionFines.FirstOrDefaultAsync(x => x.TuitionId == item.Id);

                        if (finded is not null)
                        {
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

        public async Task<List<PaymentTuition>> GetPaymentsByStudantTuitionsId(int studentId)
        {
            try
            {
                var student = await db.Students.Include(x => x.Enrollments).ThenInclude(x => x.Tuitions).FirstOrDefaultAsync(x => x.Id == studentId);
                var listOfPayments = new List<PaymentTuition>();
                var tuitions = student.Enrollments.SelectMany(x => x.Tuitions).Select(x => x.Id).ToList();
                foreach (int item in tuitions)
                {
                    var payment = await db.PaymentTuitions.FirstOrDefaultAsync(x => x.TuitionId == item);

                    if (payment is not null)
                    {
                        payment.Tuition.Enrollment = null;
                        listOfPayments.Add(payment);
                    }

                }
                return listOfPayments;
            }
            catch (Exception e)
            { 

                throw e;
            }
        }
        public async Task CreateFeePayment(CreateFeePaymentDTO dto)
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
                    db.TuitionFines.Update(TuitionFines);
                    await db.SaveChangesAsync();
                }


            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<List<PaymentTuition>> CreatePayment(CreatePaymentDTO dto)
        {
            try
            {
                var studant = await db.Students.Include(x => x.Enrollments)
                                                .ThenInclude(x => x.Tuitions)
                                               .Include(x => x.CurrentSchoolLevel)
                                               .FirstOrDefaultAsync(x => x.Id == dto.StudantId);

                if (dto.StudantId > 0 && dto.TuitionId > 0 && studant is not null)
                {
                    var payment = new Models.PaymentTuition()
                    {
                        TuitionId = dto.TuitionId,
                        PaymentDate = DateTime.Now,
                        PaymentWithoutVat = getTuitionValueByschoolLevel(studant.CurrentSchoolLevel.Description)
                    };
                    payment.VatOfPayment = VatCalc(payment.PaymentWithoutVat);
                    payment.PaymentWithVat = payment.VatOfPayment + payment.PaymentWithoutVat;

                    payment.TuitionInvoice = new TuitionInvoice()
                    {
                       
                    } ;

                    await db.SaveChangesAsync();

                    await db.PaymentTuitions.AddAsync(payment);
                    await db.SaveChangesAsync();

                    var tuitionPayed = db.Tuitions.FirstOrDefault(x => x.Id == dto.TuitionId);
                    tuitionPayed.Row = Common.Modified;
                    tuitionPayed.UpdatedDate = DateTime.Now;
                    tuitionPayed.PaidDate = DateTime.Now;
                    tuitionPayed.Paid = true;

                    db.Tuitions.Update(tuitionPayed);
                    await db.SaveChangesAsync();

                    // TODO: INVOICE 

                   
                }

                return await GetPaymentsByStudantTuitionsId(studant.Id);
            }
            catch (Exception)
            {

                throw;
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
                "Pré-escola A"  ,
                "Pré-escola B"  ,
                "Pré-escola C"
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





        public async Task CheckFee(int? studantId)
        {
            var now = DateTime.Now;

            var students = new List<Student>();

            if (studantId is not null || studantId > 0)
            {
                students = await db.Students.Include(x => x.CurrentSchoolLevel).Include(x => x.Enrollments).ThenInclude(x => x.Tuitions).Where(x => x.Row != Common.Deleted && x.Id == studantId).ToListAsync();

            }
            else
            {
                students = await db.Students.Include(x => x.CurrentSchoolLevel).Include(x => x.Enrollments).ThenInclude(x => x.Tuitions).Where(x => x.Row != Common.Deleted).ToListAsync();
            }

            foreach (Student student in students)
            {
                foreach (Tuition tuition in student.Enrollments.SelectMany(x => x.Tuitions))
                {

                    if (!tuition.Paid)
                    {


                        if (now > tuition.StartDate.AddDays(15))
                        {
                            var havetuitionFines = await db.TuitionFines.FirstOrDefaultAsync(x => x.TuitionId == tuition.Id);

                            if (now > tuition.StartDate.AddDays(14) && now <= tuition.StartDate.AddDays(24) && havetuitionFines is null)
                            {   // cria mutla de 300 se pagar entre dia 15 a 25
                                //  Console.WriteLine("300 MT");
                                var tuitionFines = new Models.Fines()
                                {
                                    TuitionId = tuition.Id
                                };
                                await db.TuitionFines.AddAsync(tuitionFines);
                                await db.SaveChangesAsync();
                            }
                            else if (now > tuition.StartDate.AddDays(24) && !student.Suspended)
                            {    // cria suspende se tiver passado 25 dias sem pagar a mensalidade
                                //  Console.WriteLine("Suspenso");
                                student.Suspended = true;
                                await db.SaveChangesAsync();
                            }
                            else if (now > tuition.StartDate.AddDays(24) && student.Suspended && havetuitionFines is null)
                            {   // cria multa se nao tiver suspenso e sem multa
                                //  Console.WriteLine("300 MT");
                                var tuitionFines = new Models.Fines()
                                {
                                    TuitionId = tuition.Id
                                };
                                await db.TuitionFines.AddAsync(tuitionFines);
                                await db.SaveChangesAsync();
                            }
                        }



                    }
                }
            }
        }

        public async Task AutomaticRegularization(int? studentId)
        {
            var Tuitions = await db.Tuitions.AnyAsync(x => x.StudentId == studentId && !x.Paid);

            var TuitionsFee = await db.Tuitions.AnyAsync(x => x.StudentId == studentId && !x.TuitionFines.Paid);

            if (!Tuitions && !TuitionsFee)
            {
                var student = await db.Students.FirstOrDefaultAsync(x => x.Id == studentId);
                student.Suspended = false; 
                await db.SaveChangesAsync();
            }

        }
    }
}

//as mensalidades devem ser antecipadamente pagas no inicio de cada Mes, do dia 1 a dia15, apartir do dia 16 a 25 a multa é de 300

//depois do dia 25 o aluno passa a ser suspenso






