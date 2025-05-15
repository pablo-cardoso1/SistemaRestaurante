using System.ComponentModel.DataAnnotations;

namespace SistemaRestaurante.DTOs;

public class MesaCriacaoDTO
{
    [Required]
    public int Numero { get; set; }
    
    [Required]
    [Range(1, int.MaxValue)]
    public int Capacidade { get; set; }
}