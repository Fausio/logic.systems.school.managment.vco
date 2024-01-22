using logic.systems.school.managment.Data;
using logic.systems.school.managment.Dto;
using logic.systems.school.managment.Interface;
using logic.systems.school.managment.Models;
using Microsoft.EntityFrameworkCore;

namespace logic.systems.school.managment.Services
{
    public class OrgUnitService : IOrgUnit
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext(new DbContextOptions<ApplicationDbContext>());

        public async Task<bool> CkeckIfCreateOrgUnitDistrictsExists(OrgUnitDistrictCreateDTO dto)
        {
            try
            {
                var getDistrict = await db.OrgUnitDistricts.FirstOrDefaultAsync(x => x.Description == dto.Description && x.OrgUnitProvinceId == dto.OrgUnitProvinceId);

                if (getDistrict == null) { return false; }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task CreateOrgUnitDistricts(OrgUnitDistrictCreateDTO dto, string createdOrUpdateUser)
        {
            try
            {
                var getDistrict = await db.OrgUnitDistricts.FirstOrDefaultAsync(x => x.Description == dto.Description && x.OrgUnitProvinceId == dto.OrgUnitProvinceId);


                if (getDistrict is not null)
                {

                }
                else
                {
                    var result = new OrgUnitDistrict
                    {
                        Description = dto.Description,
                        OrgUnitProvinceId = dto.OrgUnitProvinceId,
                        CreatedUSer = createdOrUpdateUser
                    };

                    await db.OrgUnitDistricts.AddAsync(result);
                    await db.SaveChangesAsync();
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

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

        public async Task<List<OrgUnitDistrict>> GetOrgUnitDistricts()
        {
            try
            {
                return await db.OrgUnitDistricts.OrderBy(x => x.Id).ToListAsync();
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
                return await db.OrgUnitProvinces.OrderBy(x => x.Id).ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
