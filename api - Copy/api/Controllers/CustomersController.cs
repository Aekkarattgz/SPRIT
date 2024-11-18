using Microsoft.AspNetCore.Mvc;
using BelleCroissantAPI.Data;
using BelleCroissantAPI.Model;
using System.Linq;

namespace BelleCroissantAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        // Constructor เพื่อรับ ApplicationDbContext มาใช้ในการเชื่อมต่อกับฐานข้อมูล
        public CustomersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/customers
        // ดึงข้อมูลลูกค้าทั้งหมดจากฐานข้อมูล
        [HttpGet]
        public IActionResult GetCustomers()
        {
            // ดึงข้อมูลทั้งหมดโดยตรงโดยไม่ใช้ Mapping
            var customers = _context.Customers.ToList();

            // สร้างออบเจกต์ CustomerDTO สำหรับการส่งกลับ

            return Ok(customers); // ส่งข้อมูลที่แปลงแล้วกลับไป
        }
        [HttpGet("{id}")]
        public IActionResult GetCustomerById(int id)
        {
            // ค้นหาลูกค้าตาม ID
            var customer = _context.Customers.FirstOrDefault(c => c.id == id);

            // หากไม่พบลูกค้า
            if (customer == null)
            {
                return NotFound(); // ส่งกลับ 404 Not Found
            }

            return Ok(customer); // ส่งข้อมูลลูกค้าโดยตรง
        }
        // POST: api/products
        // เพิ่มสินค้าใหม่


    }
}
