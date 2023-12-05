namespace logic.systems.school.managment.Models
{
    public class Student : Common
    {
        // Properties 
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }


        // Relationship property
        public virtual SchoolLevel CurrentSchoolLevel { get; set; }
        public int CurrentSchoolLevelId { get; set; }  

        //// New property
        //public bool IsTuitionPaid { get; set; }
    }
}
