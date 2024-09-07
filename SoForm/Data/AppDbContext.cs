using Microsoft.EntityFrameworkCore;
using SoForm.Models;

namespace SoForm.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<ProcessDbModel> Process { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("Server=localhost;Database=Process;User=root;Password=root;",
                new MySqlServerVersion(new Version(9, 0, 1))); 
        }
    }
}