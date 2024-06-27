namespace logic.systems.school.managment.Dto
{
    public class UpdateUserDTO
    {
        public string RoleName { get; set; }
        public UpdateUserInfoDTO UserInfo { get; set; } = new UpdateUserInfoDTO();
        public UpdatePasswordDTO PasswordDTO { get; set; } = new UpdatePasswordDTO();
    }
}
