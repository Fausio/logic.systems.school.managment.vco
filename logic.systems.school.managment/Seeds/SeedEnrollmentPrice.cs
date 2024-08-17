using logic.systems.school.managment.Data;
using logic.systems.school.managment.Interface;
using logic.systems.school.managment.Models;
using Microsoft.EntityFrameworkCore;

namespace logic.systems.school.managment.Seeds
{
    public static class SeedEnrollmentPrice
    {
        public static async Task Run()
        {
            await Seed();
        }

        private static async Task Seed()
        {
            var db = new ApplicationDbContext(new DbContextOptions<ApplicationDbContext>());

            if (await db.EnrollmentPrices.CountAsync() <= 0)
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
                    switch (schoolLevel)
                    {
                        case "Pré-escola":
                            listOfEnrollmentPrices.Add(new EnrollmentPrice()
                            {
                                Price = 3500,
                                Description = schoolLevel,
                            });
                            break;

                        case "1ª classe":
                            listOfEnrollmentPrices.Add(new EnrollmentPrice()
                            {
                                Price = 4000,
                                Description = schoolLevel,
                            });
                            break;

                        case "2ª classe":
                            listOfEnrollmentPrices.Add(new EnrollmentPrice()
                            {
                                Price = 3700,
                                Description = schoolLevel,
                            });
                            break;

                        case "3ª classe":
                        case "4ª classe":
                        case "5ª classe":
                        case "6ª classe":
                            listOfEnrollmentPrices.Add(new EnrollmentPrice()
                            {
                                Price = 3700,
                                Description = schoolLevel,
                            });
                            break;

                        case "7ª classe":
                            listOfEnrollmentPrices.Add(new EnrollmentPrice()
                            {
                                Price = 3700,
                                Description = schoolLevel,
                            });
                            break;

                        case "8ª classe":
                        case "9ª classe":
                        case "10ª classe":
                            listOfEnrollmentPrices.Add(new EnrollmentPrice()
                            {
                                Price = 3800,
                                Description = schoolLevel,
                            });
                            break;

                        case "11ª classe":
                            listOfEnrollmentPrices.Add(new EnrollmentPrice()
                            {
                                Price = 4200,
                                Description = schoolLevel,
                            });
                            break;

                        case "12ª classe":
                            listOfEnrollmentPrices.Add(new EnrollmentPrice()
                            {
                                Price = 4200,
                                Description = schoolLevel,
                            });
                            break;

                        default:
                            Console.WriteLine("Class");
                            break;
                    }
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
