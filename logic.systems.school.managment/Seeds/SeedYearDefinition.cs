using logic.systems.school.managment.Data;
using logic.systems.school.managment.Models;
using Microsoft.EntityFrameworkCore;

namespace logic.systems.school.managment.Seeds
{
    public static class SeedYearDefinition
    {
        public static async Task Run()
        {
            await Seed();
        }

        private static async Task Seed()
        {
            var db = new ApplicationDbContext(new DbContextOptions<ApplicationDbContext>());

            if (await db.YearDefinitions.CountAsync() <= 0)
            {
                int this_year = DateTime.Now.Year;
                int next_year = DateTime.Now.AddYears(1).Year;

                var listOfYears = new List<YearDefinition>();

                for (int i = this_year; i <= next_year; i++)
                {
                    listOfYears.Add(new YearDefinition()
                    {
                        Year = i,
                    });
                }

                if (listOfYears.Count > 0)
                {
                    await db.YearDefinitions.AddRangeAsync(listOfYears);
                    await db.SaveChangesAsync();


                    var _this_year = await db.YearDefinitions.FirstOrDefaultAsync(x => x.Year == this_year);

                    if (_this_year is not null)
                    {

                        #region enrols
                        var enrols = await db.EnrollmentPrices.ToListAsync();
                        enrols.ForEach(x =>
                        {
                            x.YearDefinitionId = _this_year.Id;


                        });

                        if (enrols.Count > 0)
                        {
                            db.EnrollmentPrices.UpdateRange(enrols);
                            await db.SaveChangesAsync();
                        }
                        #endregion

                        #region tuitions
                        var tuitions = await db.TuitionPrices.ToListAsync();
                        tuitions.ForEach(x =>
                        {
                            x.YearDefinitionId = _this_year.Id;

                        });

                        if (tuitions.Count > 0)
                        {
                            db.TuitionPrices.UpdateRange(tuitions);
                            await db.SaveChangesAsync();
                        }
                        #endregion
                    }

                }

            }
        }


    }
}
