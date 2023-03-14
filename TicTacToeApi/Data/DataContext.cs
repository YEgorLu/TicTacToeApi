using Microsoft.EntityFrameworkCore;
using TicTacToeApi.Models;

namespace TicTacToeApi.Data
{
    public class DataContext: DbContext
    {
        public DbSet<Game> Games { get; set; }
        public DbSet<Table> Tables { get; set; }
        public DbSet<Point> Points { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=superherodb;Trusted_Connection=true;TrustServerCertificate=true;");
        }
    }
}
