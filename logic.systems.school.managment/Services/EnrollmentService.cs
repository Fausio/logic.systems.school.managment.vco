using DocumentFormat.OpenXml.Drawing.Diagrams;
using logic.systems.school.managment.Data;
using logic.systems.school.managment.Interface;
using logic.systems.school.managment.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;

namespace logic.systems.school.managment.Services
{
    public class EnrollmentService : IEnrollment
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext(new DbContextOptions<ApplicationDbContext>());

        public async Task EnrollmentByStudantId(int studantId, int CurrentSchoolLevelId)
        {
            try
            {
                if (studantId > 0 && CurrentSchoolLevelId > 0)
                {
                    var enrollment = await GenerateEnrollmentDataByLevel(studantId, CurrentSchoolLevelId);
                    if (enrollment is not null)
                    {
                        await db.Enrollments.AddAsync(enrollment);
                        await db.SaveChangesAsync();
                    }
                }
            }
            catch (Exception)
            {

                throw;
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


                switch (level.Description)
                {
                    case "Pré-escola":
                        enrollment = new Enrollment()
                        {
                            StudentId = studantId,
                            PaymentEnrollment = new PaymentEnrollment()
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
                            PaymentEnrollment = new PaymentEnrollment()
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
                                    new EnrollmentItem()
                                    {
                                        Description = "Pasta",
                                        Price = 750,
                                    },
                                     new EnrollmentItem()
                                    {
                                        Description = "Caderneta",
                                        Price = 150,
                                    },
                                }
                        };
                        break;
                    case "2ª classe":
                        enrollment = new Enrollment()
                        {
                            StudentId = studantId,
                            PaymentEnrollment = new PaymentEnrollment()
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
                            PaymentEnrollment = new PaymentEnrollment()
                            {
                                
                                PaymentWithoutVat = 3700,
                            }
                        };
                        break;
                    case "7ª classe":
                        enrollment = new Enrollment()
                        {
                            StudentId = studantId,
                            PaymentEnrollment = new PaymentEnrollment()
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
                            PaymentEnrollment = new PaymentEnrollment()
                            {
                                
                                PaymentWithoutVat = 3800,
                            }
                        };
                        break;
                    case "11 classe":
                        enrollment = new Enrollment()
                        {
                            StudentId = studantId,
                            PaymentEnrollment = new PaymentEnrollment()
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
                    case "12 classe":
                        enrollment = new Enrollment()
                        {
                            StudentId = studantId,
                            PaymentEnrollment = new PaymentEnrollment()
                            {
                                
                                PaymentWithoutVat = 4200,
                            }
                        };
                        break;

                    default:
                        Console.WriteLine("Class");
                        break;
                }
                enrollment.PaymentEnrollment.Paid = true;
                enrollment.PaymentEnrollment.PaymentDate = DateTime.Now;
                return enrollment;
            }
            else
            {
                return null;
            }

        }
    }
}
