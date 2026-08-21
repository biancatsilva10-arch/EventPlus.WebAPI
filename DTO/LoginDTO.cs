using System.ComponentModel.DataAnnotations;

namespace EventPlus.WebAPI.DTO
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "O email é obrigatório para autentificação!")]
        [EmailAddress(ErrorMessage = "Informe um e-mail válido!")]
        public string Email { get; set; }
        [Required(ErrorMessage = "A senha é obrigatória para autentificação!")]
        [StringLength(60, MinimumLength = 8, ErrorMessage = "A senha deve ter entre 8 e 60 caracteres.")]
        public string Senha { get; set; } = string.Empty;
    }
}
