using logic.systems.school.managment.Models;

namespace logic.systems.school.managment.Dto
{
    public class SalesProductDTO
    {

        public Student Student { get; set; } = new Student();
        public List<Product> Products { get; set; } = new List<Product>();
    }
}
