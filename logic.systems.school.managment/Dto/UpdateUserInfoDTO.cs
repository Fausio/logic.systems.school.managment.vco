using System.ComponentModel.DataAnnotations;

namespace logic.systems.school.managment.Dto
{
    public class UpdateUserInfoDTO
    {
        public string UserId { get; set; }
        [Required(ErrorMessage = "O email é Obrigatório")]
        [EmailAddress(ErrorMessage = "Deve adicionar um email")]
        [Display(Name = "Email")]
        public string Email { get; set; }
 
    }
}
