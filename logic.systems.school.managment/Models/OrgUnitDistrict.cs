using System.ComponentModel.DataAnnotations.Schema;

namespace logic.systems.school.managment.Models
{
    [Table("OrgUnitDistrict")]
    public class OrgUnitDistrict : Common
    {
        public string Description { get; set; }

        public virtual OrgUnitProvince OrgUnitProvince { get; set; };
        public   int OrgUnitProvinceId { get; set; };
    }
}
