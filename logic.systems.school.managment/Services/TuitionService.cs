using logic.systems.school.managment.Data;
using logic.systems.school.managment.Interface;
using logic.systems.school.managment.Migrations;
using logic.systems.school.managment.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Globalization;

namespace logic.systems.school.managment.Services
{
    public class TuitionService : ITuitionService
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext(new DbContextOptions<ApplicationDbContext>());

        public string GetMonthName(int month)
        {
            CultureInfo ptCulture = new CultureInfo("pt-BR");
            DateTimeFormatInfo dtfi = DateTimeFormatInfo.GetInstance(ptCulture);
            return dtfi.GetMonthName(month);
        }
        public async Task CreateByClassOfStudant(Student model)
        {
              

            // meses de estudo (1 de fevereiro a  dezembro) para decima e decima-segunda classe, devido aos exame.
            // meses  de estudo de (1 de fevereiro a novembro) são todas as outras classes sem exame incluido sexta classe que tem exame.


            var classesWithoutExame = new List<String>()
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

            var classesWithtExame = new List<String>()
            {
              "11ª classe"  ,
              "12ª classe"

            };

            if (classesWithtExame.Contains(model.CurrentSchoolLevel .Description))
            {
                List<Tuition> tuitions = new List<Tuition>();
                string[] meses = { "Fevereiro", "Março", "Abril", "Maio", "Junho", "Julho", "Agosto", "Setembro", "Outubro", "Novembro", "Dezembro" };
                for (int i = 1; i < meses.Length + 1; i++)
                {
                    tuitions.Add(new Tuition()
                    {
                        MonthNumber = i,
                        MonthName = meses[i - 1],
                        Year = DateTime.Now.Year,
                        StudentId = model.Id,
                        AssociatedLevelId = model.CurrentSchoolLevelId

                    });
                }

                await db.Tuitions.AddRangeAsync(tuitions);
                await db.SaveChangesAsync();
            }
            if (classesWithoutExame.Contains(model.CurrentSchoolLevel .Description))
            {
                List<Tuition> tuitions = new List<Tuition>();
                string[] meses = { "Fevereiro", "Março", "Abril", "Maio", "Junho", "Julho", "Agosto", "Setembro", "Outubro", "Novembro" };
                for (int i = 1; i < meses.Length + 1; i++)
                {
                    tuitions.Add(new Tuition()
                    {
                        MonthNumber = i,
                        MonthName = meses[i - 1],
                        Year = DateTime.Now.Year,
                        StudentId = model.Id,
                        AssociatedLevelId = model.CurrentSchoolLevelId

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
    }
}








