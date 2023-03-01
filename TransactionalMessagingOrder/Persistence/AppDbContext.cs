using Microsoft.EntityFrameworkCore;
using TransactionalMessagingOrder.Entities;

namespace TransactionalMessagingOrder.Persistence
{
    public class AppDbContext : DbContext
    {
        public DbSet<InboxMessage> InboxMessages { get; set; }


        public AppDbContext()
        {
        }


        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
        }
    }
}