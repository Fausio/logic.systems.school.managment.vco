using logic.systems.school.managment.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using logic.systems.school.managment.Models;

namespace logic.systems.school.managment.Seeds
{
    public static class SeedOrgUnit
    {

        public static async Task Run()
        {
            await Seed();
        }

        private static async Task Seed()
        {
            var db = new ApplicationDbContext(new DbContextOptions<ApplicationDbContext>());


            if (await db.OrgUnitProvinces.CountAsync() <= 0)
            {
                var provinces = new List<OrgUnitProvince>()
                {
                    new OrgUnitProvince() { Id = 1, Description = "Cabo Delgado" },
                    new OrgUnitProvince() { Id = 2, Description = "Gaza" },
                    new OrgUnitProvince() { Id = 3, Description = "Inhambane" },
                    new OrgUnitProvince() { Id = 4, Description = "Manica" },
                    new OrgUnitProvince() { Id = 5, Description = "Maputo (Cidade)" },
                    new OrgUnitProvince() { Id = 6, Description = "Maputo (Província)" },
                    new OrgUnitProvince() { Id = 7, Description = "Nampula" },
                    new OrgUnitProvince() { Id = 8, Description = "Niassa" },
                    new OrgUnitProvince() { Id = 9, Description = "Sofala" },
                    new OrgUnitProvince() { Id = 10, Description = "Tete" },
                    new OrgUnitProvince() { Id = 11, Description = "Zambezia" }
                };

                await db.OrgUnitProvinces.AddRangeAsync(provinces);
                await db.SaveChangesAsync();
            }


            if (await db.OrgUnitDistricts.CountAsync() <= 0 && await db.OrgUnitProvinces.CountAsync() > 0)
            {
                var distritosData = new List<OrgUnitDistrict>
        {
            // Cabo Delgado
            new OrgUnitDistrict { Description = "Ancuabe", OrgUnitProvinceId = 1 },
            new OrgUnitDistrict { Description = "Balama", OrgUnitProvinceId = 1 },
            new OrgUnitDistrict { Description = "Chiúre", OrgUnitProvinceId = 1 },
            new OrgUnitDistrict { Description = "Ibo", OrgUnitProvinceId = 1 },
            new OrgUnitDistrict { Description = "Macomia", OrgUnitProvinceId = 1 },
            new OrgUnitDistrict { Description = "Mocímboa da Praia", OrgUnitProvinceId = 1 },
            new OrgUnitDistrict { Description = "Montepuez", OrgUnitProvinceId = 1 },
            new OrgUnitDistrict { Description = "Mueda", OrgUnitProvinceId = 1 },
            new OrgUnitDistrict { Description = "Muidumbe", OrgUnitProvinceId = 1 },
            new OrgUnitDistrict { Description = "Namuno", OrgUnitProvinceId = 1 },
            new OrgUnitDistrict { Description = "Palma", OrgUnitProvinceId = 1 },
            new OrgUnitDistrict { Description = "Pemba", OrgUnitProvinceId = 1 },
            new OrgUnitDistrict { Description = "Quissanga", OrgUnitProvinceId = 1 },

            // Gaza
            new OrgUnitDistrict { Description = "Bilene", OrgUnitProvinceId = 2 },
            new OrgUnitDistrict { Description = "Chibuto", OrgUnitProvinceId = 2 },
            new OrgUnitDistrict { Description = "Chicualacuala", OrgUnitProvinceId = 2 },
            new OrgUnitDistrict { Description = "Chigubo", OrgUnitProvinceId = 2 },
            new OrgUnitDistrict { Description = "Chókwè", OrgUnitProvinceId = 2 },
            new OrgUnitDistrict { Description = "Gaza", OrgUnitProvinceId = 2 },
            new OrgUnitDistrict { Description = "Limpopo", OrgUnitProvinceId = 2 },
            new OrgUnitDistrict { Description = "Mabalane", OrgUnitProvinceId = 2 },
            new OrgUnitDistrict { Description = "Mandlakazi", OrgUnitProvinceId = 2 },
            new OrgUnitDistrict { Description = "Massangena", OrgUnitProvinceId = 2 },
            new OrgUnitDistrict { Description = "Massingir", OrgUnitProvinceId = 2 },
            new OrgUnitDistrict { Description = "Xai-Xai", OrgUnitProvinceId = 2 },

            // Inhambane
            new OrgUnitDistrict { Description = "Funhalouro", OrgUnitProvinceId = 3 },
            new OrgUnitDistrict { Description = "Govuro", OrgUnitProvinceId = 3 },
            new OrgUnitDistrict { Description = "Homoine", OrgUnitProvinceId = 3 },
            new OrgUnitDistrict { Description = "Inharrime", OrgUnitProvinceId = 3 },
            new OrgUnitDistrict { Description = "Inhassoro", OrgUnitProvinceId = 3 },
            new OrgUnitDistrict { Description = "Jangamo", OrgUnitProvinceId = 3 },
            new OrgUnitDistrict { Description = "Mabote", OrgUnitProvinceId = 3 },
            new OrgUnitDistrict { Description = "Massinga", OrgUnitProvinceId = 3 },
            new OrgUnitDistrict { Description = "Maxixe", OrgUnitProvinceId = 3 },
            new OrgUnitDistrict { Description = "Panda", OrgUnitProvinceId = 3 },
            new OrgUnitDistrict { Description = "Quissico", OrgUnitProvinceId = 3 },
            new OrgUnitDistrict { Description = "Vilanculos", OrgUnitProvinceId = 3 },
            new OrgUnitDistrict { Description = "Zavala", OrgUnitProvinceId = 3 },

            // Manica
            new OrgUnitDistrict { Description = "Bárue", OrgUnitProvinceId = 4 },
            new OrgUnitDistrict { Description = "Gondola", OrgUnitProvinceId = 4 },
            new OrgUnitDistrict { Description = "Macate", OrgUnitProvinceId = 4 },
            new OrgUnitDistrict { Description = "Machaze", OrgUnitProvinceId = 4 },
            new OrgUnitDistrict { Description = "Macossa", OrgUnitProvinceId = 4 },
            new OrgUnitDistrict { Description = "Manica", OrgUnitProvinceId = 4 },
            new OrgUnitDistrict { Description = "Mossurize", OrgUnitProvinceId = 4 },
            new OrgUnitDistrict { Description = "Sussundenga", OrgUnitProvinceId = 4 },

            // Maputo (Cidade)
            new OrgUnitDistrict { Description = "KaMpfumo", OrgUnitProvinceId = 5 },
            new OrgUnitDistrict { Description = "Mavalane", OrgUnitProvinceId = 5 },
            new OrgUnitDistrict { Description = "Zimpeto", OrgUnitProvinceId = 5 },

            // Maputo (Província)
            new OrgUnitDistrict { Description = "Boane", OrgUnitProvinceId = 6 },
            new OrgUnitDistrict { Description = "Magude", OrgUnitProvinceId = 6 },
            new OrgUnitDistrict { Description = "Manhiça", OrgUnitProvinceId = 6 },
            new OrgUnitDistrict { Description = "Matola", OrgUnitProvinceId = 6 },
            new OrgUnitDistrict { Description = "Namaacha", OrgUnitProvinceId = 6 },

            // Nampula
            new OrgUnitDistrict { Description = "Angoche", OrgUnitProvinceId = 7 },
            new OrgUnitDistrict { Description = "Eráti", OrgUnitProvinceId = 7 },
            new OrgUnitDistrict { Description = "Ilha de Moçambique", OrgUnitProvinceId = 7 },
            new OrgUnitDistrict { Description = "Lalaua", OrgUnitProvinceId = 7 },
            new OrgUnitDistrict { Description = "Mecubúri", OrgUnitProvinceId = 7 },
            new OrgUnitDistrict { Description = "Mogovolas", OrgUnitProvinceId = 7 },
            new OrgUnitDistrict { Description = "Moma", OrgUnitProvinceId = 7 },
            new OrgUnitDistrict { Description = "Monapo", OrgUnitProvinceId = 7 },
            new OrgUnitDistrict { Description = "Mossuril", OrgUnitProvinceId = 7 },
            new OrgUnitDistrict { Description = "Nacala-a-Velha", OrgUnitProvinceId = 7 },
            new OrgUnitDistrict { Description = "Nacala-Porto", OrgUnitProvinceId = 7 },
            new OrgUnitDistrict { Description = "Nampula", OrgUnitProvinceId = 7 },
            new OrgUnitDistrict { Description = "Ribaue", OrgUnitProvinceId = 7 },

            // Niassa
            new OrgUnitDistrict { Description = "Cuamba", OrgUnitProvinceId = 8 },
            new OrgUnitDistrict { Description = "Lago", OrgUnitProvinceId = 8 },
            new OrgUnitDistrict { Description = "Lichinga", OrgUnitProvinceId = 8 },
            new OrgUnitDistrict { Description = "Majune", OrgUnitProvinceId = 8 },
            new OrgUnitDistrict { Description = "Mandimba", OrgUnitProvinceId = 8 },
            new OrgUnitDistrict { Description = "Marrupa", OrgUnitProvinceId = 8 },
            new OrgUnitDistrict { Description = "Maúa", OrgUnitProvinceId = 8 },
            new OrgUnitDistrict { Description = "N'gauma", OrgUnitProvinceId = 8 },
            new OrgUnitDistrict { Description = "Sanga", OrgUnitProvinceId = 8 },
            new OrgUnitDistrict { Description = "Sussundenga", OrgUnitProvinceId = 8 },

            // Sofala
            new OrgUnitDistrict { Description = "Beira", OrgUnitProvinceId = 9 },
            new OrgUnitDistrict { Description = "Búzi", OrgUnitProvinceId = 9 },
            new OrgUnitDistrict { Description = "Caia", OrgUnitProvinceId = 9 },
            new OrgUnitDistrict { Description = "Chibabava", OrgUnitProvinceId = 9 },
            new OrgUnitDistrict { Description = "Dondo", OrgUnitProvinceId = 9 },
            new OrgUnitDistrict { Description = "Gorongosa", OrgUnitProvinceId = 9 },
            new OrgUnitDistrict { Description = "Machanga", OrgUnitProvinceId = 9 },
            new OrgUnitDistrict { Description = "Marromeu", OrgUnitProvinceId = 9 },
            new OrgUnitDistrict { Description = "Maringué", OrgUnitProvinceId = 9 },
            new OrgUnitDistrict { Description = "Muanza", OrgUnitProvinceId = 9 },
            new OrgUnitDistrict { Description = "Nhamatanda", OrgUnitProvinceId = 9 },
            new OrgUnitDistrict { Description = "Vanduzi", OrgUnitProvinceId = 9 },

            // Tete
            new OrgUnitDistrict { Description = "Angónia", OrgUnitProvinceId = 10 },
            new OrgUnitDistrict { Description = "Cahora-Bassa", OrgUnitProvinceId = 10 },
            new OrgUnitDistrict { Description = "Changara", OrgUnitProvinceId = 10 },
            new OrgUnitDistrict { Description = "Chifunde", OrgUnitProvinceId = 10 },
            new OrgUnitDistrict { Description = "Macanga", OrgUnitProvinceId = 10 },
            new OrgUnitDistrict { Description = "Magoe", OrgUnitProvinceId = 10 },
            new OrgUnitDistrict { Description = "Marara", OrgUnitProvinceId = 10 },
            new OrgUnitDistrict { Description = "Mutarara", OrgUnitProvinceId = 10 },
            new OrgUnitDistrict { Description = "Tete", OrgUnitProvinceId = 10 },
            new OrgUnitDistrict { Description = "Tsangano", OrgUnitProvinceId = 10 },
            new OrgUnitDistrict { Description = "Zumbo", OrgUnitProvinceId = 10 },

            // Zambezia
            new OrgUnitDistrict { Description = "Alto Molócuè", OrgUnitProvinceId = 11 },
            new OrgUnitDistrict { Description = "Chinde", OrgUnitProvinceId = 11 },
            new OrgUnitDistrict { Description = "Gile", OrgUnitProvinceId = 11 },
            new OrgUnitDistrict { Description = "Gurúè", OrgUnitProvinceId = 11 },
            new OrgUnitDistrict { Description = "Ile", OrgUnitProvinceId = 11 },
            new OrgUnitDistrict { Description = "Inhassunge", OrgUnitProvinceId = 11 },
            new OrgUnitDistrict { Description = "Luabo", OrgUnitProvinceId = 11 },
            new OrgUnitDistrict { Description = "Maganja da Costa", OrgUnitProvinceId = 11 },
            new OrgUnitDistrict { Description = "Mopeia", OrgUnitProvinceId = 11 },
            new OrgUnitDistrict { Description = "Morrumbala", OrgUnitProvinceId = 11 },
            new OrgUnitDistrict { Description = "Namacurra", OrgUnitProvinceId = 11 },
            new OrgUnitDistrict { Description = "Namacurra Velha", OrgUnitProvinceId = 11 },
            new OrgUnitDistrict { Description = "Nante", OrgUnitProvinceId = 11 },
            new OrgUnitDistrict { Description = "Nicoadala", OrgUnitProvinceId = 11 },
            new OrgUnitDistrict { Description = "Pebane", OrgUnitProvinceId = 11 },
            new OrgUnitDistrict { Description = "Quelimane", OrgUnitProvinceId = 11 },

            // Adicione mais distritos conforme necessário...
        };

                await db.OrgUnitDistricts.AddRangeAsync(distritosData);
                await db.SaveChangesAsync();
            }

        }
    }
}
