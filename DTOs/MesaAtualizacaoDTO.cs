using System.ComponentModel.DataAnnotations;

namespace SistemaRestaurante.DTOs;

public class MesaAtualizacaoDTO
{
    [Required]
    public int Numero { get; set; }

    [Required]
    [Range(1, int.MaxValue)]
    public int Capacidade { get; set; }

    [Required]
    public string Status { get; set; } // "disponível", "reservada", "inativa"
}