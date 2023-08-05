using Microsoft.EntityFrameworkCore;
using TaskPlanner.Models;

namespace TaskPlanner.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }

        public DbSet<User> Users { get; set; }
        public DbSet<PlannedTasks> PlannedTasks { get; set; }
        public DbSet<Activities> Activities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasOne<PlannedTasks>(u => u.PlannedTasks)
                .WithOne(pt => pt.User)
                .HasForeignKey<PlannedTasks>(pt => pt.UserId);

            modelBuilder.Entity<PlannedTasks>()
                .HasMany<Activities>(pt => pt.Activities)
                .WithOne(a => a.PlannedTasks)
                .HasForeignKey(pt => pt.PlannedTasksId);
        }
    }
}
