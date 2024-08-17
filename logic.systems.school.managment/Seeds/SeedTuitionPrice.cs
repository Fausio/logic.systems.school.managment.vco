using logic.systems.school.managment.Data;
using logic.systems.school.managment.Models;
using Microsoft.EntityFrameworkCore;

namespace logic.systems.school.managment.Seeds
{
    public static class SeedTuitionPrice
    {
        public static async Task Run()
        {
            await Seed();
        }

        private static async Task Seed()
        {
            var db = new ApplicationDbContext(new DbContextOptions<ApplicationDbContext>());

            if (await db.TuitionPrices.CountAsync() <= 0)
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

                var listOfTuitionPrice = new List<TuitionPrice>();

                listOfSchoolLevel.ForEach(x =>
                {
                    listOfTuitionPrice.Add(new TuitionPrice()
                    {
                        Price = getTuitionValueByschoolLevel(x),
                        Description = x,
                    });

                });

                await db.TuitionPrices.AddRangeAsync(listOfTuitionPrice);
                await db.SaveChangesAsync();
            }
        }

        private static decimal getTuitionValueByschoolLevel(string schoolLevel)
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
