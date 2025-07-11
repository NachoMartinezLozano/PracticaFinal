using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace PracticaFinalApi.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<OperacionItem> Operaciones { get; set; } // DbSet para las operaciones
        public DbSet<EquipoItem> Equipos { get; set; } // DbSet para los equipos
        public DbSet<AgenteItem> Agentes { get; set; } // DbSet para los agentes
        public DbSet<TokenInfo> TokenInfos { get; set; } // DbSet para los tokens de acceso

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OperacionItem>()
                .HasMany(o => o.Equipos)
                .WithOne(e => e.Operacion)
                .HasForeignKey(e => e.OperacionId)
                .OnDelete(DeleteBehavior.SetNull); // Eliminación en cascada

            // Relación EquipoItem -> AgenteItem (uno a muchos)
            modelBuilder.Entity<EquipoItem>()
                .HasMany(e => e.Agentes)
                .WithOne(a => a.Equipo)
                .HasForeignKey(a => a.EquipoId)
                .OnDelete(DeleteBehavior.SetNull); // Eliminación en cascada

            base.OnModelCreating(modelBuilder);
        }
    }
}