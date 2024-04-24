using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Drawing.Diagrams;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using logic.systems.school.managment.Data;
using logic.systems.school.managment.Dto;
using logic.systems.school.managment.Interface;
using logic.systems.school.managment.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace logic.systems.school.managment.Services
{
    public class EnrollmentService : IEnrollment
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext(new DbContextOptions<ApplicationDbContext>());
        private ITuitionService _ITuitionService;
        private IstudantService _StudentService;
        public EnrollmentService(ITuitionService iTuitionService, IstudantService studentService)
        {
            this._ITuitionService = iTuitionService;
            _StudentService = studentService;

        }




        public async Task<Enrollment> EnrollmentByStudantId(int studantId, int CurrentSchoolLevelId, int EnrollmentYear, int SchoolClassRoomId, decimal EnrollmentPrice, decimal TuitionPrice)
        {
            try
            {
                if (studantId > 0 && CurrentSchoolLevelId > 0)
                {
                    var enrollment = await GenerateEnrollmentDataByLevel(studantId, CurrentSchoolLevelId, EnrollmentPrice);

                    if (enrollment is not null)
                    {
                        enrollment.TuitionPrice = TuitionPrice;
                        var PaymentEnrollment = enrollment.PaymentEnrollment;

                        await db.Enrollments.AddAsync(enrollment);
                        await db.SaveChangesAsync();

                        enrollment.PaymentEnrollmentId = enrollment.PaymentEnrollment.Id;
                        enrollment.EnrollmentYear = EnrollmentYear;
                        enrollment.SchoolClassRoomId = SchoolClassRoomId;
                        await db.SaveChangesAsync();

                        var invoice = new EnrollmentInvoice()
                        {
                            EnrollmentId = enrollment.Id,
                            Invoice = new Invoice() { }

                        };

                        await db.EnrollmentInvoices.AddAsync(invoice);
                        await db.SaveChangesAsync();
                        await GeneratePedgogicalData(enrollment, enrollment.CreatedUSer);
                        return enrollment;
                    }
                    else
                    {
                        throw new Exception("Nof found enrollment");
                    }
                }
                else
                {
                    throw new Exception("Nof found EnrollmentByStudantId");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<EnrollmentListDTO>> EnrollmentsByStudantId(EnrollmentCreateDTO model, string userId)
        {


            var enrollment = await EnrollmentByStudantId(model.StudantId, model.SchoolLevelId, model.EnrollmentYear, model.SchoolClassRoomId, model.EnrollmentPrice, model.TuitionPrice);


            await _ITuitionService.CreateByClassOfStudant(await _StudentService.Read(model.StudantId), enrollment, userId);
            var student = await db.Students.FirstOrDefaultAsync(x => x.Id == model.StudantId);
            student.CurrentSchoolLevelId = model.SchoolLevelId;
            await db.SaveChangesAsync();

            return await EnrollmentsByStudantId(model.StudantId);
        }
        public async Task<List<EnrollmentListDTO>> EnrollmentsByStudantId(int studantId)
        {
            try
            {
                var result = new List<EnrollmentListDTO>();

                result = (from e in db.Enrollments.Include(x => x.EnrollmentItems)
                          join level in db.SimpleEntitys on e.SchoolLevelId equals level.Id
                          join pay in db.PaymentEnrollments on e.Id equals pay.EnrollmentId
                          where e.StudentId == studantId
                          select new EnrollmentListDTO
                          {
                              id = e.Id,
                              createdDate = e.CreatedDate,
                              level = level.Description,
                              year = e.EnrollmentYear,
                              items = getEnrollmentItems(e.EnrollmentItems).Result,
                              value = (pay.PaymentWithoutVat - getotalItems(e.EnrollmentItems).Result).ToString(),
                              Total = pay.PaymentWithoutVat.ToString(),
                          }).ToList();

                return result;
            }
            catch (Exception e)
            {

                throw e;
            }
        }




        public async Task<bool> CheckIfHaveEnrollmentIntheYear(EnrollmentCreateDTO model)
          => await db.Enrollments.AnyAsync(x => x.EnrollmentYear == model.EnrollmentYear && x.StudentId == model.StudantId);









        private static async Task<string> getEnrollmentItems(List<EnrollmentItem> enrolemntItens)
        {

            var itens = string.Empty;

            if (enrolemntItens != null && enrolemntItens.Count() > 0)
            {
                foreach (var item in enrolemntItens)
                {
                    itens = itens + $" item: {item.Description}, preço: {item.Price} MT  <br>";
                }
                return itens;
            }
            else
            {
                return itens;
            }
        }
        private static async Task<decimal> getotalItems(List<EnrollmentItem> enrolemntItens)
        {

            decimal itensPrices = 0;

            if (enrolemntItens != null && enrolemntItens.Count() > 0)
            {
                foreach (var item in enrolemntItens)
                {
                    itensPrices = itensPrices + item.Price;
                }
                return itensPrices;
            }
            else
            {
                return 0;
            }
        }

        private async Task GeneratePedgogicalData(Enrollment enrollment, string userId)
        {
            var quarter = new List<Quarter>();
            quarter.Add(new Quarter()
            {
                Number = 1,
                CreatedUSer = enrollment.CreatedUSer
            }); quarter.Add(new Quarter()
            {
                Number = 2,
                CreatedUSer = enrollment.CreatedUSer
            }); quarter.Add(new Quarter()
            {
                Number = 3,
                CreatedUSer = enrollment.CreatedUSer
            });


            switch (enrollment.SchoolLevel.Description)
            {

                #region 1 a 3
                case "1ª classe":
                case "2ª classe":
                case "3ª classe":

                    List<string> Subjects_1_3 = new List<string>
                        {
                            "Português",
                            "Matemática",
                            "Inglês",
                            "Francês",
                            "Educação física"
                        };


                    foreach (var q in quarter)
                    {

                        foreach (var subjectParamiter in Subjects_1_3)
                        {
                            var subjectId = await db.SimpleEntitys.FirstOrDefaultAsync(x => x.Type == SimpleEntity.Type_Subject && x.Description == subjectParamiter);
                            q.Assessments.Add(
                                     new Assessment(true)
                                     {
                                         CreatedUSer = userId,
                                         SubjectId = subjectId.Id
                                     }
                             );
                        }
                    }
                    break;
                #endregion




                #region 4
                case "4ª classe":

                    List<string> Subjects_4 = new List<string>
                        {
                       "Português",
    "Matemática",
    "Inglês",
    "Francês",
    "Ciências sociais",
    "Ciências naturais",
    "TIC's (Tecnologias da Informação e Comunicação)",
    "Educação física"
                        };


                    foreach (var q in quarter)
                    {

                        foreach (var subjectParamiter in Subjects_4)
                        {
                            var subjectId = await db.SimpleEntitys.FirstOrDefaultAsync(x => x.Type == SimpleEntity.Type_Subject && x.Description == subjectParamiter);
                            q.Assessments.Add(
                                                              new Assessment(true)
                                                              {
                                                                  CreatedUSer = userId,
                                                                  SubjectId = subjectId.Id
                                                              }
                                                      );
                        }
                    }
                    break;
                #endregion






                #region 5 a 6
                case "5ª classe":
                case "6ª classe":

                    List<string> Subjects_5_6 = new List<string>
                        {
    "Português",
    "Matemática",
    "Inglês",
    "Francês",
    "Ciências sociais",
    "Ciências naturais",
    "TIC's (Tecnologias da Informação e Comunicação)",
    "Educação visual & ofício",
    "Educação física"
                        };


                    foreach (var q in quarter)
                    {

                        foreach (var subjectParamiter in Subjects_5_6)
                        {
                            var subjectId = await db.SimpleEntitys.FirstOrDefaultAsync(x => x.Type == SimpleEntity.Type_Subject && x.Description == subjectParamiter);
                            q.Assessments.Add(
                                                              new Assessment(true)
                                                              {
                                                                  CreatedUSer = userId,
                                                                  SubjectId = subjectId.Id
                                                              }
                                                      );
                        }
                    }
                    break;
                #endregion

                     
                #region 7
                case "7ª classe":

                    List<string> Subjects_7 = new List<string>
                        {
"Português",
    "Matemática",
    "Inglês",
    "Francês",
    "História",
    "Geografia",
    "TIC's (Tecnologias da Informação e Comunicação)",
    "Biologia",
    "Noções de contabilidade",
    "Agropecuária",
    "Educação visual",
    "Educação física"
                        };


                    foreach (var q in quarter)
                    {

                        foreach (var subjectParamiter in Subjects_7)
                        {
                            var subjectId = await db.SimpleEntitys.FirstOrDefaultAsync(x => x.Type == SimpleEntity.Type_Subject && x.Description == subjectParamiter);
                            q.Assessments.Add(
                                                              new Assessment(true)
                                                              {
                                                                  CreatedUSer = userId,
                                                                  SubjectId = subjectId.Id
                                                              }
                                                      );
                        }
                    }
                    break;
                #endregion
 
                #region 8, 9 e 10
                case "8ª classe":
                case "9ª classe":
                case "10ª classe":

                    List<string> Subjects_8_9_10 = new List<string>
                        {
    "Português",
    "Matemática",
    "Biologia",
    "Química",
    "Física",
    "Geografia",
    "História",
    "Educação visual",
    "TIC's (Tecnologias da Informação e Comunicação)",
    "Agropecuária",
    "Noções de contabilidade",
    "Francês",
    "Inglês",
    "Educação física"
                        };


                    foreach (var q in quarter)
                    {
                        foreach (var subjectParamiter in Subjects_8_9_10)
                        {
                            var subjectId = await db.SimpleEntitys.FirstOrDefaultAsync(x => x.Type == SimpleEntity.Type_Subject && x.Description == subjectParamiter);
                            q.Assessments.Add(
                                                              new Assessment(true)
                                                              {
                                                                  CreatedUSer = userId,
                                                                  SubjectId = subjectId.Id
                                                              }
                                                      );
                        }
                    }
                    break;
                #endregion










                #region 11
                case "11ª classe":

                    List<string> Subjects_11 = new List<string>
                        {
    "Português",
    "Matemática",
    "Inglês",
    "Francês",
    "Geografia",
    "Biologia",
    "Química",
    "Filosofia",
    "TIC's (Tecnologias da Informação e Comunicação)",
    "Física",
    "Desenho geométrico descritivo",
    "Educação física"
                        };


                    foreach (var q in quarter)
                    {

                        foreach (var subjectParamiter in Subjects_11)
                        {
                            var subjectId = await db.SimpleEntitys.FirstOrDefaultAsync(x => x.Type == SimpleEntity.Type_Subject && x.Description == subjectParamiter);
                            q.Assessments.Add(
                                                              new Assessment(true)
                                                              {
                                                                  CreatedUSer = userId,
                                                                  SubjectId = subjectId.Id
                                                              }
                                                      );
                        }
                    }
                    break;
                #endregion




                default:
                    Console.WriteLine("Class");
                    break;
            }




            foreach (var item in quarter)
            {
                item.EnrollmentId = enrollment.Id;




            }

            await db.Quarters.AddRangeAsync(quarter);
            await db.SaveChangesAsync();
        }


        private async Task<Enrollment> GenerateEnrollmentDataByLevel(int studantId, int SchoolLevelId, decimal EnrollmentPrice)
        {

            var level = await db.SimpleEntitys.FirstOrDefaultAsync(x => x.Id == SchoolLevelId);
            var student = await db.Students.FirstOrDefaultAsync(x => x.Id == studantId);

            if (level is not null)
            {
                var enrollment = new Enrollment();


                enrollment = new Enrollment()
                {
                    StudentId = studantId,
                    PaymentEnrollment = new EnrollmentPayment()
                    {
                        PaymentWithoutVat = EnrollmentPrice,
                    },
                };
                enrollment.EnrollmentPrice = EnrollmentPrice;
                enrollment.SchoolLevelId = SchoolLevelId;
                enrollment.PaymentEnrollment.Paid = true;
                enrollment.PaymentEnrollment.PaymentDate = DateTime.Now;

                return enrollment;
            }
            else
            {
                return null;
            }

        }

        public async Task DeleteParmanentyById(int Id)
        {
            try
            {
                var obj = await db.Enrollments.Include(x => x.EnrollmentItems).FirstOrDefaultAsync(x => x.Id == Id);

                if (obj != null)
                {
                    var tuitions = await db.Tuitions.Where(x => x.EnrollmentId == obj.Id).ToListAsync();
                    if (tuitions.Count > 0)
                    {
                        db.Tuitions.RemoveRange(tuitions);
                        await db.SaveChangesAsync();
                    }

                    var invoice = await db.EnrollmentInvoices.Where(x => x.EnrollmentId == obj.Id).ToListAsync();
                    if (invoice.Count > 0)
                    {
                        db.EnrollmentInvoices.RemoveRange(invoice);
                        await db.SaveChangesAsync();
                    }

                    var payments = await db.PaymentEnrollments.Where(x => x.EnrollmentId == obj.Id).ToListAsync();
                    if (payments.Count > 0)
                    {
                        db.PaymentEnrollments.RemoveRange(payments);
                        await db.SaveChangesAsync();
                    }

                    if (obj.EnrollmentItems != null && obj.EnrollmentItems.Count > 0)
                    {
                        var items = obj.EnrollmentItems.ToList();
                        db.EnrollmentItems.RemoveRange(items);
                        await db.SaveChangesAsync();
                    }

                    var quarters = await db.Quarters.Where(x => x.EnrollmentId == obj.Id).ToListAsync();
                    if (quarters.Count > 0)
                    {
                        foreach (var quarter in quarters)
                        {
                            var assessments = await db.Assessments.Where(x => x.QuarterId == quarter.Id).ToListAsync();

                            foreach (var assessment in assessments)
                            {
                                var grade = await db.Grades.Where(x => x.AssessmentId == assessment.Id).ToListAsync();

                                db.Grades.RemoveRange(grade);
                                await db.SaveChangesAsync();
                            }

                            db.Assessments.RemoveRange(assessments);
                            await db.SaveChangesAsync();
                        }

                        db.Quarters.RemoveRange(quarters);
                        await db.SaveChangesAsync();
                    }

                    db.Enrollments.Remove(obj);
                    await db.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                // Tratar a exceção aqui, como registrar ou lidar com ela de alguma forma apropriada para sua aplicação.
                Console.WriteLine($"Ocorreu uma exceção: {ex.Message}");
                // throw; // Pode ser removido ou mantido, dependendo da necessidade.
            }




        }

        public async Task UpdatePrices(EditStudantDTO model)
        {
            var enrolments = await db.Enrollments.Include(x=> x.PaymentEnrollment).OrderBy(x=> x.Id).LastOrDefaultAsync(x => x.StudentId == model.id);
            if (enrolments is not null)
            {
                var commite = false;

                if (enrolments.TuitionPrice != model.TuitionPrice)
                {
                    enrolments.TuitionPrice = model.TuitionPrice;
                    commite = true;
                } 
                
                if (enrolments.EnrollmentPrice != model.EnrollmentPrice)
                {
                    enrolments.EnrollmentPrice = model.EnrollmentPrice;
                    enrolments.PaymentEnrollment.PaymentWithoutVat = model.EnrollmentPrice;
                    commite = true;
                }

                if (commite)
                { 
                    enrolments.UpdatedDate = DateTime.Now;
                    db.Enrollments.Update(enrolments);
                    await db.SaveChangesAsync(true);
                }
       
            }
        }
    }

}