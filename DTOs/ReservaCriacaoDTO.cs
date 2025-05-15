using System.ComponentModel.DataAnnotations;

namespace SistemaRestaurante.DTOs;

public class ReservaCriacaoDTO
{
    [Required]
    public int MesaId { get; set; }

    [Required]
    public DateTime DataReserva { get; set; }

    [Required]
    [Range(1, int.MaxValue)]
    public int QuantidadePessoas { get; set; }
}