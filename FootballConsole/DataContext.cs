using FootballLibrary;
using Microsoft.EntityFrameworkCore;

namespace FootballConsole
{
    class DataContext : DbContext
    {
        public DbSet<Player> Players { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Transfer> Transfers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-TIM\SQLEXPRESS;Initial Catalog=hogent_entityframework;Integrated Security=True;Pooling=False");
        }
    }
}
