namespace logic.systems.school.managment.Dto
{
    public class ReportDataFilterDTO
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }


        public ReportDataFilterDTO()
        {
            StartDate = DateTime.Now.AddDays(-1);
            EndDate = DateTime.Now;
        }
    }
}
