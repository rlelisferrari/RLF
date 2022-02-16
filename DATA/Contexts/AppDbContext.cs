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

            modelBuilder.Entity<ScoutGeral>()
                .HasKey(scout => new { scout.idAtleta, scout.idJogo });

            modelBuilder.Entity<ScoutGeral>()
                .HasOne(scout => scout.atleta)
                .WithMany(atleta => atleta.ScoutGeral)
                .HasForeignKey(scout => scout.idAtleta);

            modelBuilder.Entity<ScoutGeral>()
                .HasOne(scout => scout.jogo)
                .WithMany(jogo => jogo.ScoutGeral)
                .HasForeignKey(scout => scout.idJogo);
        }

        public DbSet<Ordem> Ordens { get; set; }
        public DbSet<Equipamento> Equipamentos { get; set; }
        public DbSet<TipoOrdem> TipoOrdens { get; set; }
        public DbSet<TipoEquipamento> TipoEquipamento { get; set; }
        public DbSet<EquipamentoTipoEquipamento> EquipamentoTipoEquipamento { get; set; }
        public DbSet<VeiculoOlx> VeiculosOlx { get; set; }
        public DbSet<Atleta> Atletas { get; set; }
        public DbSet<Jogo> Jogos { get; set; }
        public DbSet<ScoutGeral> ScoutsGerais { get; set; }
    }
}