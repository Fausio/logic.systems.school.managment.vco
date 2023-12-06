namespace logic.systems.school.managment.Models
{
    public abstract class Common
    {
        public int Id { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; } 
        public  DataRowState Row { get; set; }


        public Common()
        {
            Row = DataRowState.New;
        }
    }


    public enum DataRowState
    {
        New,
        Modified,
        Deleted,
        Unchanged
    }
}
