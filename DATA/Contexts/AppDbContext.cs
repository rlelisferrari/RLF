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
        }

        public DbSet<Ordem> Ordens { get; set; }
        public DbSet<Equipamento> Equipamentos { get; set; }
        public DbSet<TipoOrdem> TipoOrdens { get; set; }
    }
}