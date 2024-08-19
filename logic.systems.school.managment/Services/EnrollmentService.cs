using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Drawing.Diagrams;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using logic.systems.school.managment.Data;
using logic.systems.school.managment.Dto;
using logic.systems.school.managment.Interface;
using logic.systems.school.managment.Models;
using Microsoft.AspNetCore.Http;
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

        public async Task<List<EnrollmentPrice>> ReadEnrolmentPrices()
        {
            var result = await db.EnrollmentPrices.Include(x => x.YearDefinition).Include(x => x.EnrollmentItemstPrice).ToListAsync();

            return result;
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
                              value = pay.PaymentWithoutVat.ToString(),
                              Total = (pay.PaymentWithoutVat + getotalItems(e.EnrollmentItems).Result).ToString(),
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


                var enrolmentPrice = await ReadEnrolmentPriceByDescription(level.Description);

                if (enrolmentPrice is not null)
                {
                    enrollment = new Enrollment()
                    {
                        StudentId = studantId,
                        PaymentEnrollment = new EnrollmentPayment()
                        {
                            PaymentWithoutVat = enrolmentPrice.Price,
                        },

                    };

                    if (enrolmentPrice.EnrollmentItemstPrice.Count() > 0)
                    {
                        enrollment.EnrollmentItems = new List<EnrollmentItem>()
                                {
                                    new EnrollmentItem()
                                    {
                                        Description = enrolmentPrice.EnrollmentItemstPrice[0].Description,
                                        Price = enrolmentPrice.EnrollmentItemstPrice[0].Price,
                                    }
                                };
                    }
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

        public async Task<EnrollmentPrice> ReadEnrolmentPriceById(int id)
        {
            var result = await db.EnrollmentPrices.Include(x => x.YearDefinition).FirstOrDefaultAsync(x => x.Id == id);


            if (result.EnrollmentPriceHistory.Count <= 0)
            {
                List<EnrollmentPriceHistory> findIhistory = await db.EnrollmentPriceHistorys.Where(x => x.EnrollmentPriceeId == id).OrderByDescending(x => x.Id).ToListAsync();

                if (findIhistory.Count > 0)
                {
                    result.EnrollmentPriceHistory = findIhistory;
                }
            }


            if (result.EnrollmentItemstPrice.Count <= 0)
            {
                List<EnrollmentItemstPrice> findItens = await db.EnrollmentItemstPrices.Where(x => x.EnrollmentPriceId == id).ToListAsync();

                if (findItens.Count > 0)
                {
                    result.EnrollmentItemstPrice = findItens;
                }
            }

            return result;
        }

        public async Task<EnrollmentPrice> ReadEnrolmentPriceByDescription(string description)
        {
            var result = await db.EnrollmentPrices.FirstOrDefaultAsync(x => x.Description == description);

            if (result.EnrollmentItemstPrice.Count <= 0)
            {
                List<EnrollmentItemstPrice> findItens = await db.EnrollmentItemstPrices.Where(x => x.EnrollmentPriceId == result.Id).ToListAsync();

                if (findItens.Count > 0)
                {
                    result.EnrollmentItemstPrice = findItens;
                }
            }

            return result;
        }




        public async Task<EnrollmentPrice> UpdateEnrollmentPrice(EnrollmentPrice entity)
        {
            var result = await ReadEnrolmentPriceById(entity.Id);

            if (result.Price == entity.Price)
            {
                return await ReadEnrolmentPriceById(entity.Id);
            }

            if (result is not null)
            {

                var historyDescription = @$"Utilizador {entity.UpdatedUSer} altertou a matricula da {result.Description} de {result.Price.ToString("N2")} para {entity.Price.ToString("N2")} em {DateTime.Now.ToString("dd/MM/yyyy")}";

                result.EnrollmentPriceHistory.Add(new EnrollmentPriceHistory()
                {
                    Description = historyDescription,
                    EnrollmentPriceeId = result.Id,

                });

                result.Price = entity.Price;
                db.EnrollmentPrices.Update(result);
                await db.SaveChangesAsync();
            }

            return await ReadEnrolmentPriceById(entity.Id);
        }


        public async Task<List<YearDefinition>> ReadYearDefinitions()
        {

            List<YearDefinition> result = await db.YearDefinitions
                                           .Include(x => x.EnrollmentPrices).ThenInclude(x => x.EnrollmentPriceHistory)
                                           .Include(x => x.EnrollmentPrices).ThenInclude(x => x.EnrollmentItemstPrice)
                                           .ToListAsync();

            return result;
        }

        public async Task generateEnrolmentPrice(int YearIdid)
        {

            var year = await db.YearDefinitions.FirstOrDefaultAsync(x => x.Id == YearIdid);

            if (year is not null && year.EnrollmentPrices.Count <= 0)
            {

                var listOfSchoolLevel = new List<string>()
                {
                    "Pré-escola",
                    "1ª classe",
                    "2ª classe",
                    "3ª classe",
                    "4ª classe",
                    "5ª classe",
                    "6ª classe",
                    "7ª classe",
                    "8ª classe",
                    "9ª classe",
                    "10ª classe",
                    "11ª classe",
                    "12ª classe",
                };

                var listOfEnrollmentPrices = new List<EnrollmentPrice>();

                // Primeira parte: Gravar os EnrollmentPrice
                listOfSchoolLevel.ForEach(schoolLevel =>
                {
                    listOfEnrollmentPrices.Add(new EnrollmentPrice()
                    {
                        Price = 0,
                        Description = schoolLevel,
                        YearDefinitionId = year.Id,
                    });

                });

                await db.EnrollmentPrices.AddRangeAsync(listOfEnrollmentPrices);
                await db.SaveChangesAsync();

                // Segunda parte: Gravar os EnrollmentItemstPrice com os EnrollmentPriceId
                foreach (var enrollmentPrice in listOfEnrollmentPrices)
                {
                    List<EnrollmentItemstPrice> enrollmentItemstPrices = null;

                    switch (enrollmentPrice.Description)
                    {
                        case "Pré-escola":
                            enrollmentItemstPrices = new List<EnrollmentItemstPrice>
                            {
                                new EnrollmentItemstPrice
                                {
                                    Description = "Fichas",
                                    Price = 1000,
                                    EnrollmentPriceId = enrollmentPrice.Id
                                }
                            };
                            break;

                        case "1ª classe":
                            enrollmentItemstPrices = new List<EnrollmentItemstPrice>
                            {
                                new EnrollmentItemstPrice
                                {
                                    Description = "Fichas",
                                    Price = 1000,
                                    EnrollmentPriceId = enrollmentPrice.Id
                                }
                            };
                            break;

                        case "2ª classe":
                            enrollmentItemstPrices = new List<EnrollmentItemstPrice>
                            {
                                new EnrollmentItemstPrice
                                {
                                    Description = "Fichas",
                                    Price = 1000,
                                    EnrollmentPriceId = enrollmentPrice.Id
                                }
                            };
                            break;

                        case "7ª classe":
                            enrollmentItemstPrices = new List<EnrollmentItemstPrice>
                            {
                                new EnrollmentItemstPrice
                                {
                                    Description = "Certidão",
                                    Price = 1500,
                                    EnrollmentPriceId = enrollmentPrice.Id
                                }
                            };
                            break;

                        case "11ª classe":
                            enrollmentItemstPrices = new List<EnrollmentItemstPrice>
                            {
                                new EnrollmentItemstPrice
                                {
                                    Description = "Certidão",
                                    Price = 1500,
                                    EnrollmentPriceId = enrollmentPrice.Id
                                }
                            };
                            break;
                    }

                    if (enrollmentItemstPrices != null)
                    {
                        await db.EnrollmentItemstPrices.AddRangeAsync(enrollmentItemstPrices);
                    }
                }

                await db.SaveChangesAsync();
            }
        }
    }

}