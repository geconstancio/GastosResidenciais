using Microsoft.EntityFrameworkCore;
using GastosResidenciais.Api.Models;

namespace GastosResidenciais.Api.Data;

/// <summary>Sessão de acesso ao banco SQLite via EF Core.</summary>
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

    public DbSet<Pessoa> Pessoas { get; set; } = null!;

    public DbSet<Transacao> Transacoes { get; set; } = null!;
    /// <summary>Configurações de mapeamento que a convenção do EF Core não cobre.</summary>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    // Certifica que ao deletar uma pessoa todas as suas transações serão apagadas junto dela
    // evitando registros órfãos no banco.
        modelBuilder.Entity<Transacao>()
        .HasOne(t => t.Pessoa)
        .WithMany(p => p.Transacoes)
        .HasForeignKey(t => t.PessoaId)
        .OnDelete(DeleteBehavior.Cascade);
    }
}