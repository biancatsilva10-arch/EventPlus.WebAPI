using System.ComponentModel.DataAnnotations;

namespace EventPlus.WebAPI.DTO;

/// <summary>
/// Data Transfer Obejct (DTO) para cadastro e atualização do Perfil/Tipo de Usuario.
/// </summary>

    public class TipoUsuarioDTO
    {
    /// <summary>
    /// titulo do tipo de usuario
    /// </summary>
    [Required(ErrorMessage ="O título é obrigatório.")]
    [StringLength(100, ErrorMessage = "O título pode ter no máximo 100 caracteres")]
     public string Titulo { get; set; } = string.Empty;
    public object IdTipoUsuario { get; internal set; }
}

