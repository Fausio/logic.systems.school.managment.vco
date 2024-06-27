using System.ComponentModel.DataAnnotations;

namespace logic.systems.school.managment.Models
{
    
        public class RegisterModel
        {
            [Required(ErrorMessage = "O email é Obrigatório")]
            [EmailAddress(ErrorMessage = "Deve adicionar um email")]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required(ErrorMessage = "O senha é Obrigatório")]
            [StringLength(100, ErrorMessage = "O {0} deve ter pelo menos {2} e no máximo {1} caracteres.", MinimumLength = 4)]
            [DataType(DataType.Password)]
            [Display(Name = "Senha")]
            public string Password { get; set; }

            [Required(ErrorMessage = "A Localização do utilizador é Obrigatória")]
            public int ProvinceId { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirmar senha")]
            [Compare("Password", ErrorMessage = "A senha e a confirmação de senha não coincidem.")]
            public string ConfirmPassword { get; set; }
         
    }
}
