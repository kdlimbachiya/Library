using Microsoft.EntityFrameworkCore;

namespace LibraryCoreApp.Models
{
    public class AppDbContext : DbContext
    {
        //Constructor calling the Base DbContext Class Constructor
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        //OnConfiguring() method is used to select and configure the data source
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

        //Adding Domain Classes as DbSet Properties
        public DbSet<User> Users { get; set; }
        public DbSet<Books> BooksData { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
    }
}
