using Microsoft.EntityFrameworkCore;
using PoupaDev.API.Entities;

namespace PoupaDev.API.Persistence
{
  public class PoupaDevDbContext : DbContext
  {

    public PoupaDevDbContext(DbContextOptions<PoupaDevDbContext> options) : base(options)
    { }
    public DbSet<ObjetivoFinanceiro> Objetivo { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
      builder.Entity<ObjetivoFinanceiro>(e =>
      {
        e.HasKey(o => o.Id);

        e.Property(o => o.Valor).HasColumnType("decimal(18,4)");

        e.HasMany(o => o.Operacoes)
        .WithOne()
        .HasForeignKey(o => o.IdObjetivo)
        .OnDelete(DeleteBehavior.Restrict);
      });

      builder.Entity<Operacao>(e =>
      {
        e.HasKey(o => o.Id);

        e.Property(o => o.Valor).HasColumnType("decimal(18,4)");
      });
    }
  }
}
