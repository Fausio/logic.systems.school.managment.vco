namespace logic.systems.school.managment.Dto
{
    public class UserPageDto
    {
        public string? UserName { get; set; }
        public PaginationDTO<Models.AppUser> indexPage { get; set; }

    }
}
