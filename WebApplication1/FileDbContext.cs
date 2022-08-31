using Microsoft.EntityFrameworkCore;

namespace WebApplication1
{
    public class FileDbContext : DbContext
    {
        private readonly string connectionString;

        public FileDbContext( string connectionString)
        {
            this.connectionString = connectionString;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(connectionString);
            base.OnConfiguring(optionsBuilder);
        }

        //private const string CONNECTION_STRING = "Host=localhost;Port=5432;" +
        //            "Username=postgres;" +
        //            "Password=postgres;" +
        //            "Database=FileDb";

        public DbSet<File> Files { get; set; }
    }
}
