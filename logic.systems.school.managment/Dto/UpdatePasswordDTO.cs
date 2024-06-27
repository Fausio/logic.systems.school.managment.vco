using System.ComponentModel.DataAnnotations;

namespace logic.systems.school.managment.Dto
{
    public class UpdatePasswordDTO
    {
        public string UserId { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória")]
        [MinLength(4, ErrorMessage = "A senha deve ter no mínimo 4 caracteres")]
        public string NewPassword { get; set; }
    }
}
