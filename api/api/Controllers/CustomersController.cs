using Microsoft.AspNetCore.Mvc;
using BelleCroissantAPI.Data;
using BelleCroissantAPI.Model;

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
            var customers = _context.Customers.ToList();
            return Ok(customers); // คืนค่าข้อมูลทั้งหมดในรูปแบบ JSON พร้อมสถานะ 200 OK
        }

        // GET: api/customers/{id}
        // ดึงข้อมูลลูกค้าตาม id ที่ระบุ
        [HttpGet("{id}")]
        public IActionResult GetCustomer(int id)
        {
            var customer = _context.Customers.Find(id);
            if (customer == null) return NotFound(); // หากไม่พบลูกค้า
            return Ok(customer); // คืนค่าลูกค้าในรูปแบบ JSON พร้อมสถานะ 200 OK
        }

        // POST: api/customers
        // เพิ่มข้อมูลลูกค้าใหม่
        [HttpPost]
        public IActionResult AddCustomer([FromBody] Customer customer)
        {
            _context.Customers.Add(customer); // เพิ่มข้อมูลลูกค้าใหม่เข้าไปในฐานข้อมูล
            _context.SaveChanges(); // บันทึกข้อมูลลงฐานข้อมูล
            return CreatedAtAction(nameof(GetCustomer), new { id = customer.CustomerId }, customer); // คืนค่าการสร้างใหม่พร้อมลิงก์ไปยังข้อมูลที่ถูกเพิ่ม
        }

        // PUT: api/customers/{id}
        // อัปเดตข้อมูลลูกค้าที่มี id ตามที่ระบุ
        [HttpPut("{id}")]
        public IActionResult UpdateCustomer(int id, [FromBody] Customer customer)
        {
            var existingCustomer = _context.Customers.Find(id); // ค้นหาลูกค้าตาม id ที่ระบุ
            if (existingCustomer == null) return NotFound(); // หากไม่พบลูกค้า

            // อัปเดตข้อมูลของลูกค้า
            existingCustomer.Name = customer.Name;
            existingCustomer.Email = customer.Email;

            _context.SaveChanges(); // บันทึกการเปลี่ยนแปลงลงฐานข้อมูล
            return NoContent(); // คืนค่า 204 No Content (ไม่มีเนื้อหากลับไป)
        }
    }
}
