using BelleCroissantAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace BelleCroissantAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        // ตารางต่างๆ ที่ต้องการใช้ในฐานข้อมูล
        public DbSet<Product> Products { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Order> Orders { get; set; }
    }
}
