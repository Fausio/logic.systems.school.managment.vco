using System.ComponentModel.DataAnnotations.Schema;

namespace logic.systems.school.managment.Models
{
    [Table("Student")]
    public class Student : Common
    {
        // Properties  
        public string Name { get; set; }  
        public DateTime BirthDate { get; set; } 
        public string FatherName { get; set; }
        public string MatherName { get; set; } 
        public string PersonId { get; set; }

        // Relationship property
        public virtual SchoolLevel CurrentSchoolLevel { get; set; }
        public int CurrentSchoolLevelId { get; set; }

        public virtual OrgUnitDistrict District { get; set; }
        public int DistrictId { get; set; }

        public virtual Sponsor Sponsor { get; set; }
        public int SponsorId { get; set; }

     

        //// New property
        //public bool IsTuitionPaid { get; set; }
    }
}
