namespace logic.systems.school.managment.Dto
{
    public class BeneficiariesSuspededReportDTO
    {
       
        public int StudendId { get; set; }
        public string StudendName { get; set; }
        public string StudendGender { get; set; }
        public string StudendBirthDate { get; set; }
        public string StudentClassLevel { get; set; }
        public List<BeneficiariesSuspededReportItemDTO> items { get; set; } = new List<BeneficiariesSuspededReportItemDTO>();

    }
}
