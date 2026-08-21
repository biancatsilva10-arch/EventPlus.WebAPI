using System.ComponentModel.DataAnnotations;

namespace EventPlus.WebAPI.DTO
{
    public class InstituicaoDTO
    {
        [Required(ErrorMessage = "Campo obrigatório!")]
        [StringLength(100, ErrorMessage = "O NomeFantasia deve ter no máximo 100 caracteres")]
        public string NomeFantasia { get; set; } = string.Empty;

        [Required(ErrorMessage = "Campo obrigatório!")]
        [StringLength(18, MinimumLength = 18, ErrorMessage = "O CNPJ deve ter 18 caracteres.")]
        public string CNPJ { get; set; } = string.Empty;

        [Required(ErrorMessage = "O endereço é obrigatório!")]
        [StringLength(150, MinimumLength = 5, ErrorMessage = "O endereço deve ter entre 5 e 150 caracteres.")]
        public string Endereco { get; set; } = string.Empty;
    }
}


