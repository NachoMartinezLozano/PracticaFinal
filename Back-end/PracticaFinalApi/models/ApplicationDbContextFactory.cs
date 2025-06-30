using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using PracticaFinalApi.Models;

namespace PracticaFinalApi
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlite("Data Source=PracticaFinal.db");

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}