using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using WeatherForecast.Models;

namespace WeatherForecast.Repository
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base()
        {
        }

        public DbSet<Cidade> Cidade { get; set; }
        public DbSet<Estado> Estado { get; set; }
        public DbSet<PrevisaoClima> PrevisaoClima { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Cidade>()
                .HasRequired(x => x.Estado)
                .WithMany(x => x.Cidade)
                .HasForeignKey(x => x.EstadoId);

            modelBuilder.Entity<Estado>()
                .HasMany(x => x.Cidade)
                .WithRequired(x => x.Estado)
                .HasForeignKey(x => x.EstadoId);

            modelBuilder.Entity<Cidade>()
                .HasMany(x => x.PrevisaoClima)
                .WithOptional(x => x.Cidade)
                .HasForeignKey(x => x.CidadeId);

            modelBuilder.Entity<PrevisaoClima>()
                .HasRequired(x => x.Cidade)
                .WithMany(x => x.PrevisaoClima)
                .HasForeignKey(x => x.CidadeId);
        }
    }
}