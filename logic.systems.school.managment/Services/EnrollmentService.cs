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




        public async Task<Enrollment> EnrollmentByStudantId(int studantId, int CurrentSchoolLevelId, int EnrollmentYear, int SchoolClassRoomId)
        {
            try
            {
                if (studantId > 0 && CurrentSchoolLevelId > 0)
                {
                    var enrollment = await GenerateEnrollmentDataByLevel(studantId, CurrentSchoolLevelId);
                    if (enrollment is not null)
                    {
                        var PaymentEnrollment = enrollment.PaymentEnrollment;
                        await db.Enrollments.AddAsync(enrollment);
                        await db.SaveChangesAsync();

                        enrollment.PaymentEnrollmentId = enrollment.PaymentEnrollment.Id;
                        enrollment.EnrollmentYear = EnrollmentYear;
                        enrollment.SchoolClassRoomId = SchoolClassRoomId;
                        await db.SaveChangesAsync();

                        var invoice = new EnrollmentInvoice()
                        {
                            EnrollmentId = enrollment.Id
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


            var enrollment = await EnrollmentByStudantId(model.StudantId, model.SchoolLevelId, model.EnrollmentYear, model.SchoolClassRoomId);


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


        private async Task<Enrollment> GenerateEnrollmentDataByLevel(int studantId, int SchoolLevelId)
        {

            var level = await db.SimpleEntitys.FirstOrDefaultAsync(x => x.Id == SchoolLevelId);

            if (level is not null)
            {
                var enrollment = new Enrollment();

                #region logic em pt language
                // Pre - 3500 matricula  1000 fichas     = 4500 
                // 1- 4000 matricula 1000 fichas 750 pasta  150 caderneta = 5750 
                // 2-  - 3700 matricula  1000 fichas     = 4700
                // [3-6] -  3700 matricula
                // 7 -  3700 matricula 1500 certidão = 5200 
                // [8-10] - 3800 matricula
                // 11 - 4200 matricula 1500 certidao = 5700
                // 12 - 4200 matricula   
                #endregion

                switch (level.Description)
                {
                    case "Pré-escola":
                        enrollment = new Enrollment()
                        {
                            StudentId = studantId,
                            PaymentEnrollment = new EnrollmentPayment()
                            {
                                PaymentWithoutVat = 3500,
                            },
                            EnrollmentItems = new List<EnrollmentItem>()
                                {
                                    new EnrollmentItem()
                                    {
                                        Description = "Fichas",
                                        Price = 1000,
                                    }
                                }
                        };
                        break;
                    case "1ª classe":
                        enrollment = new Enrollment()
                        {
                            StudentId = studantId,
                            PaymentEnrollment = new EnrollmentPayment()
                            {
                                PaymentWithoutVat = 4000,
                            },
                            EnrollmentItems = new List<EnrollmentItem>()
                                {
                                    new EnrollmentItem()
                                    {
                                        Description = "Fichas",
                                        Price = 1000,
                                    },
                                }
                        };
                        break;
                    case "2ª classe":
                        enrollment = new Enrollment()
                        {
                            StudentId = studantId,
                            PaymentEnrollment = new EnrollmentPayment()
                            {
                                PaymentWithoutVat = 3700,
                            },
                            EnrollmentItems = new List<EnrollmentItem>()
                                {
                                    new EnrollmentItem()
                                    {
                                        Description = "Fichas",
                                        Price = 1000,
                                    }
                                }
                        };
                        break;

                    case "3ª classe":
                    case "4ª classe":
                    case "5ª classe":
                    case "6ª classe":
                        enrollment = new Enrollment()
                        {
                            StudentId = studantId,
                            PaymentEnrollment = new EnrollmentPayment()
                            {
                                PaymentWithoutVat = 3700,
                            }
                        };
                        break;
                    case "7ª classe":
                        enrollment = new Enrollment()
                        {
                            StudentId = studantId,
                            PaymentEnrollment = new EnrollmentPayment()
                            {

                                PaymentWithoutVat = 3700,
                            },
                            EnrollmentItems = new List<EnrollmentItem>()
                                {
                                    new EnrollmentItem()
                                    {
                                        Description = "Certidão",
                                        Price = 1500,
                                    }
                                }
                        };
                        break;

                    case "8ª classe":
                    case "9ª classe":
                    case "10ª classe":
                        enrollment = new Enrollment()
                        {
                            StudentId = studantId,
                            PaymentEnrollment = new EnrollmentPayment()
                            {
                                PaymentWithoutVat = 3800,
                            }
                        };
                        break;
                    case "11ª classe":
                        enrollment = new Enrollment()
                        {
                            StudentId = studantId,
                            PaymentEnrollment = new EnrollmentPayment()
                            {
                                PaymentWithoutVat = 4200,
                            },
                            EnrollmentItems = new List<EnrollmentItem>()
                                {
                                    new EnrollmentItem()
                                    {
                                        Description = "Certidão",
                                        Price = 1500,
                                    }
                                }
                        };
                        break;
                    case "12ª classe":
                        enrollment = new Enrollment()
                        {
                            StudentId = studantId,
                            PaymentEnrollment = new EnrollmentPayment()
                            {
                                PaymentWithoutVat = 4200,
                            }
                        };
                        break;

                    default:
                        Console.WriteLine("Class");
                        break;
                }
                enrollment.SchoolLevelId = SchoolLevelId;
                enrollment.PaymentEnrollment.Paid = true;
                enrollment.PaymentEnrollment.PaymentDate = DateTime.Now;

                var student = await db.Students.FirstOrDefaultAsync(x => x.Id == studantId);

                if (!student.Internal)
                {
                    enrollment.EnrollmentItems.Add(
                     new EnrollmentItem()
                     {
                         Description = "Caderneta e Pasta",
                         Price = 750,
                     });
                }


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
    }

}