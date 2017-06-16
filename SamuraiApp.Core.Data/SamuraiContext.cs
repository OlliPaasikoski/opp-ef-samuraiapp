
using Microsoft.EntityFrameworkCore;
using SamuraiApp.Core.Domain;

namespace SamuraiApp.Core.Data
{
    public class SamuraiContext : DbContext 
    {
        public DbSet<Samurai> Samurais { get; set; }
        public DbSet<Battle> Battles { get; set; }
        public DbSet<Quote> Quotes { get; set; } 

        protected override void OnConfiguring(DbContextOptionsBuilder builder) 
        {
            builder.UseSqlServer(
                "Server=tcp:oppdev-sql.database.windows.net,1433;Initial Catalog=Samuraidev;Persist Security Info=False;User ID=bosky;Password=3KNUGA91NUlb;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"  
            );

            /* SQLite implementation
            builder.UseSqlite(
                "DataSource=.\\SamuraiDB.db"
            ); */

            // Server=tcp:oppdev-sql.database.windows.net,1433;Initial Catalog=Samuraidev;Persist Security Info=False;User ID=bosky;Password=3KNUGA91NUlb;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;
        }               
    }
}