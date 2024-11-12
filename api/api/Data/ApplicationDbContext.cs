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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // กำหนด Primary Key สำหรับ Order
            modelBuilder.Entity<Order>()
                .HasKey(o => o.OrderId);  // กำหนดให้ OrderId เป็น Primary Key

            // กำหนด Primary Key สำหรับ Product
            modelBuilder.Entity<Product>()
                .HasKey(p => p.ProductId);  // กำหนด ProductId เป็น Primary Key

            // กำหนดชนิดข้อมูล decimal สำหรับ Price ใน Product
            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18,2)");

            // กำหนด Primary Key สำหรับ Customer
            modelBuilder.Entity<Customer>()
                .HasKey(c => c.CustomerId);  // กำหนด CustomerId เป็น Primary Key

            // คุณสามารถกำหนดการตั้งค่าอื่นๆ สำหรับโมเดลได้ที่นี่
        }
    }
}
