namespace logic.systems.school.managment.Dto
{
    public class UpdateUserDTO
    {
        public UpdateUserInfoDTO UserInfo { get; set; } = new UpdateUserInfoDTO();
        public UpdatePasswordDTO PasswordDTO { get; set; } = new UpdatePasswordDTO();
    }
}
