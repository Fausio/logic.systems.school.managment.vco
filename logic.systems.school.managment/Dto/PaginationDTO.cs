namespace logic.systems.school.managment.Dto
{
    public class PaginationDTO<T>
    {
        public string searchString { get; set; }
        public int pageNumber { get; set; }
        public int pageSize { get; set; } 
        public int totalRecords { get; set; }
        public double totalPages { get; set; } 
        public List<T> records { get; set; }

    }
}
