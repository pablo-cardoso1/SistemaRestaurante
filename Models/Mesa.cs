using System.ComponentModel.DataAnnotations;

namespace SistemaRestaurante.Models;

public class Mesa
{
    public int Id { get; set; }
    
    [Required]
    public int Numero { get; set; }
    
    [Required]
    public int Capacidade { get; set; }
    
    [Required]
    public string Status { get; set; }
    
    
    public ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
    
}