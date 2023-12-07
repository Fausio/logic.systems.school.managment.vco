namespace logic.systems.school.managment.Models
{
    public abstract class Common
    {
        public const string New = "New";
        public const string Modified = "Modified";
        public const string Deleted = "Deleted";
        public const string Unchanged = "Unchanged";

        public int Id { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string Row { get; set; }

        public string CreatedUSer { get; set; }
        public string? UpdatedUSer { get; set; }


        public Common()
        {
            Row = New;
            CreatedDate = DateTime.UtcNow;
            CreatedUSer = "8e445865-a24d-4543-a6c6-9443d048cdb9";
        }
    }



}
