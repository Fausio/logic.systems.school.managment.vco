using logic.systems.school.managment.Dto;
using logic.systems.school.managment.Models;

namespace logic.systems.school.managment.Interface
{
    public interface IOrgUnit
    {
        public Task<List<OrgUnitProvince>> GetOrgUnitProvinces();
        public Task<List<OrgUnitDistrict>> GetOrgUnitDistricts();
        public Task<List<OrgUnitDistrict>> GetOrgUnitDistrictsByProvinceId(int OrgUnitProvinceId);
        public Task<OrgUnitDistrict> GetOrgUnitDistrictByProvinceId(int OrgUnitProvinceId);

        public Task CreateOrgUnitDistricts(OrgUnitDistrictCreateDTO dto, string createdUser);

        public Task<bool> CkeckIfCreateOrgUnitDistrictsExists(OrgUnitDistrictCreateDTO dto);

    }
}
