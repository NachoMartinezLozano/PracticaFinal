using Microsoft.EntityFrameworkCore;

namespace PracticaFinalApi.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<OperacionItem> Operaciones { get; set; } // DbSet para las operaciones
        public DbSet<EquipoItem> Equipos { get; set; } // DbSet para los equipos
        public DbSet<AgenteItem> Agentes { get; set; } // DbSet para los agentes
    }
}