using logic.systems.school.managment.Data;
using logic.systems.school.managment.Interface;
using logic.systems.school.managment.Models;
using Microsoft.EntityFrameworkCore;

namespace logic.systems.school.managment.Services
{
    public class OrgUnitService : IOrgUnit
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext(new DbContextOptions<ApplicationDbContext>());

        public async Task<OrgUnitDistrict> GetOrgUnitDistrictByProvinceId(int OrgUnitProvinceId)
        {
            try
            {
                return await db.OrgUnitDistricts.FirstOrDefaultAsync(x => x.OrgUnitProvinceId == OrgUnitProvinceId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<OrgUnitDistrict>> GetOrgUnitDistrictsByProvinceId(int OrgUnitProvinceId)
        {
            try
            {
                return await db.OrgUnitDistricts.Where(x => x.OrgUnitProvinceId == OrgUnitProvinceId).ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<OrgUnitProvince>> GetOrgUnitProvinces()
        {
            try
            {
                return  await db.OrgUnitProvinces.OrderBy(x => x.Id).ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
