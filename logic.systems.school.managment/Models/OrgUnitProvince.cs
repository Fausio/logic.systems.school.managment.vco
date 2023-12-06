using System.ComponentModel.DataAnnotations.Schema;

namespace logic.systems.school.managment.Models
{
    [Table("OrgUnitProvince")]
    public class OrgUnitProvince : Common
    {
        public string Description { get; set; }
    }
}
