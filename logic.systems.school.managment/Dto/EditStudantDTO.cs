namespace logic.systems.school.managment.Dto
{
    public class EditStudantDTO:CreateStudantDTO
    {
        public int id { get; set; }
        public bool  Suspended { get; set; }
    }
}
