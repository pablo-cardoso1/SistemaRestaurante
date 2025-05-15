namespace SistemaRestaurante.DTOs;

public class ReservaRespostaDTO
{
    public int Id { get; set; }
    public int MesaId { get; set; }
    public int UsuarioId { get; set; }
    public DateTime DataReserva { get; set; }
    public int QuantidadePessoas { get; set; }
    public string Status { get; set; }
}