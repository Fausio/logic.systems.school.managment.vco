namespace logic.systems.school.managment.Models
{
    public class Tuition : Common
    {
        public SchoolLevel AssociatedLevel { get; set; }
        public SchoolLevel AssociatedLevelId { get; set; }
        public decimal MonthlyFee { get; set; }
    }
}
