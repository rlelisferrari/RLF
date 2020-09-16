using DOMAIN.Models;
using Microsoft.EntityFrameworkCore;

namespace DATA.Contexts
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<EquipamentoTipoEquipamento>()
                .HasKey(bc => new { bc.EquipamentoId, bc.TipoEquipamentoId });

            modelBuilder.Entity<EquipamentoTipoEquipamento>()
                .HasOne(bc => bc.Equipamento)
                .WithMany(b => b.EquipamentoTipoEquipamento)
                .HasForeignKey(bc => bc.EquipamentoId);

            modelBuilder.Entity<EquipamentoTipoEquipamento>()
                .HasOne(bc => bc.TipoEquipamento)
                .WithMany(c => c.EquipamentoTipoEquipamento)
                .HasForeignKey(bc => bc.TipoEquipamentoId);
        }

        public DbSet<Ordem> Ordens { get; set; }
        public DbSet<Equipamento> Equipamentos { get; set; }
        public DbSet<TipoOrdem> TipoOrdens { get; set; }
    }
}