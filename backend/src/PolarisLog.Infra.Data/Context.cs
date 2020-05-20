using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PolarisLog.Domain.Entities;

namespace PolarisLog.Infra
{
    public class Context : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Ambiente> Ambientes { get; set; }
        public DbSet<Nivel> Niveis { get; set; }
        public DbSet<Log> Logs { get; set; }

        public Context(DbContextOptions<Context> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("AspNetUsers");
                entity.HasKey(usuario => usuario.Id);
            });

            modelBuilder.Entity<Ambiente>(entity =>
            {
                entity.HasKey(ambiente => ambiente.Id);

                entity.HasData(new Ambiente("Produção"), new Ambiente("Homologação"), new Ambiente("Dev"));
            });

            modelBuilder.Entity<Nivel>(entity =>
            {
                entity.HasKey(nivel => nivel.Id);

                entity.HasData(
                    new Nivel("Verbose"),
                    new Nivel("Debug"),
                    new Nivel("Information"),
                    new Nivel("Warning"),
                    new Nivel("Error"),
                    new Nivel("Fatal")
                );
            });

            modelBuilder.Entity<Log>(entity =>
            {
                entity.HasKey(log => log.Id);
                entity.Property(log => log.CadastradoEm);
                entity.HasOne(log => log.Usuario);
                entity.HasOne(log => log.Ambiente);
                entity.HasOne(log => log.Nivel);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}