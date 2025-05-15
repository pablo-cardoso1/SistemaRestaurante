using System.ComponentModel.DataAnnotations;

namespace SistemaRestaurante.Models;

public class Reserva
{
    public int Id { get; set; }

    [Required]
    public int MesaId { get; set; }
    
    [Required]
    public int UsuarioId { get; set; }

    [Required]
    public DateTime DataReserva { get; set; }

    [Required]
    public string Status { get; set; }
    
    [Required]
    public int QuantidadePessoas { get; set; }
    
    
    public Mesa Mesa { get; set; }
    public Usuario Usuario { get; set; }
}