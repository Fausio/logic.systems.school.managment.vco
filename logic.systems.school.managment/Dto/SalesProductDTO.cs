using logic.systems.school.managment.Models;

namespace logic.systems.school.managment.Dto
{
    public class SalesProductDTO
    {

        public Student Student { get; set; } = new Student();
        public List<ProductDropDownDTO> Products { get; set; } = new List<ProductDropDownDTO>();
    }
}
