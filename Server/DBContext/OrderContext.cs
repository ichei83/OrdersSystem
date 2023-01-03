using Microsoft.EntityFrameworkCore;
using OrdersSystem.Models;

namespace OrdersSystem.DBContext
{
    public class OrderContext : DbContext
    {
        public OrderContext(DbContextOptions<OrderContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<UserRole>().ToTable("UsersRole");
            modelBuilder.Entity<Order>().ToTable("Orders");
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<UserRole> UsersRole { get; set; }
    }
}
