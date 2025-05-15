using System.ComponentModel.DataAnnotations;

namespace SistemaRestaurante.Models;

public class Usuario
{
    public int Id { get; set; }

    [Required] [MaxLength(80)] public string Nome { get; set; }

    [Required] [EmailAddress] public string Email { get; set; }

    [Required] public string SenhaHash { get; set; }

    [Required] public string Role { get; set; }


    public ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
}    