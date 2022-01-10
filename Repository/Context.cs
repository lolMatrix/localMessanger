using Entity;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    class Context : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<MessageGroup> MessageGroups { get; set; }
        public DbSet<Message> Messages { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=sema;Username=postgres;Password=root");
        }
    }
}
