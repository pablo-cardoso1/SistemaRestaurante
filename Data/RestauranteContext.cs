using Microsoft.EntityFrameworkCore;
using SistemaRestaurante.Models;

namespace SistemaRestaurante.Data;

public class RestauranteContext : DbContext
{
    public RestauranteContext(DbContextOptions<RestauranteContext> options) : base(options) { }
    
    public DbSet<Mesa> Mesas { get; set; }
    public DbSet<Reserva> Reservas { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Mesa>().ToTable("Mesa");
        
        
        //Relacionamento
        modelBuilder.Entity<Reserva>()
            .HasOne(r => r.Usuario)
            .WithMany(u => u.Reservas)
            .HasForeignKey(r => r.UsuarioId)
            .OnDelete(DeleteBehavior.Restrict); //Evita deletar o usuário se tiver uma reserva dele
        
        modelBuilder.Entity<Reserva>()
            .HasOne(r => r.Mesa)
            .WithMany(m => m.Reservas)
            .HasForeignKey(r => r.MesaId)
            .OnDelete(DeleteBehavior.Restrict); //Evita deletar a mesa se tiver uma reserva nela
        
        
        // Email único
        modelBuilder.Entity<Usuario>()
            .HasIndex(u => u.Email).IsUnique();
        
        base.OnModelCreating(modelBuilder); // garante todas as configurações padrão do Entity Framework sejam mantidas
    }
}