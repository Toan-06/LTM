using Microsoft.EntityFrameworkCore;
using Server.Models;

namespace Server.Data
{
    // Database Context tối giản nhất có thể
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // Kết nối trực tiếp tới file SQLite
            options.UseSqlite("Data Source=rDB.sqlite");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Đảm bảo Username không bị trùng
            modelBuilder.Entity<User>().HasIndex(u => u.Username).IsUnique();
        }
    }
}
