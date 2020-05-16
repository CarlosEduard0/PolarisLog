using Microsoft.EntityFrameworkCore;
using PolarisLog.Domain.Entities;

namespace PolarisLog.Infra
{
    public class Context : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Log> Logs { get; set; }
        
        public Context(DbContextOptions<Context> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(usuario => usuario.Id);
                entity.Property(usuario => usuario.CadastradoEm);
            });
            
            modelBuilder.Entity<Log>(entity =>
            {
                entity.HasKey(log => log.Id);
                entity.Property(log => log.CadastradoEm);
                entity.HasOne(log => log.Usuario).WithMany();
            });
            base.OnModelCreating(modelBuilder);
        }
    }
}