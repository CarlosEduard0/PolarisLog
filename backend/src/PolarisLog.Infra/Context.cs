using Microsoft.EntityFrameworkCore;
using PolarisLog.Domain.Entities;

namespace PolarisLog.Infra
{
    public class Context : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }
        
        public Context(DbContextOptions<Context> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>().HasKey(usuario => usuario.Id);
            base.OnModelCreating(modelBuilder);
        }
    }
}