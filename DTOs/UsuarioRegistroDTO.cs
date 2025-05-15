using System.ComponentModel.DataAnnotations;

namespace SistemaRestaurante.DTOs;

public class UsuarioRegistroDTO
{
    [Required]
    [MaxLength(80)]
    public string Nome { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [MinLength(6)]
    public string Senha { get; set; }
    
}