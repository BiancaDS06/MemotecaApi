using MemotecaApi.Models;
using Microsoft.EntityFrameworkCore;
//Conexão cm o db
namespace MemotecaApi.Data
{
    public class AppDataContext : DbContext
    {
        public DbSet<Thought> Thoughts { get; set; }
        
        protected override void OnConfiguring(
            DbContextOptionsBuilder optionsBuilder) 
            => optionsBuilder.UseSqlite(connectionString: "DataSource=app.db;Cache=Shared");
    }
}