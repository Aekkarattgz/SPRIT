using Microsoft.AspNetCore.Mvc;
using BelleCroissantAPI.Data;
using BelleCroissantAPI.Model;

namespace BelleCroissantAPI.Controllers
{
    // กำหนดเส้นทางของ API สำหรับการจัดการคำสั่งซื้อ
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        // Constructor สำหรับการเชื่อมต่อกับฐานข้อมูล
        public OrdersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/orders
        // ดึงข้อมูลคำสั่งซื้อทั้งหมดจากฐานข้อมูล
        [HttpGet]
        public IActionResult GetOrders()
        {
            var orders = _context.Orders.ToList(); // ดึงข้อมูลคำสั่งซื้อทั้งหมด
            return Ok(orders); // คืนค่าผลลัพธ์ในรูปแบบ JSON พร้อมสถานะ 200 OK
        }

        // GET: api/orders/{id}
        // ดึงข้อมูลคำสั่งซื้อจาก id ที่ระบุ
        [HttpGet("{id}")]
        public IActionResult GetOrder(int id)
        {
            var order = _context.Orders.Find(id); // ค้นหาคำสั่งซื้อจาก id
            if (order == null) return NotFound(); // ถ้าไม่พบคำสั่งซื้อให้คืนค่า 404 Not Found
            return Ok(order); // คืนค่าคำสั่งซื้อในรูปแบบ JSON พร้อมสถานะ 200 OK
        }

        // POST: api/orders
        // เพิ่มคำสั่งซื้อใหม่
        [HttpPost]
        public IActionResult AddOrder([FromBody] Order order)
        {
            _context.Orders.Add(order); // เพิ่มคำสั่งซื้อใหม่
            _context.SaveChanges(); // บันทึกคำสั่งซื้อใหม่ลงฐานข้อมูล
            return CreatedAtAction(nameof(GetOrder), new { id = order.OrderId }, order); // คืนค่าการสร้างใหม่พร้อมลิงก์ไปยังคำสั่งซื้อที่เพิ่ม
        }

        // PUT: api/orders/{id}/complete
        // เปลี่ยนสถานะคำสั่งซื้อให้เป็น "Completed"
        [HttpPut("{id}/complete")]
        public IActionResult CompleteOrder(int id)
        {
            var order = _context.Orders.Find(id); // ค้นหาคำสั่งซื้อจาก id
            if (order == null) return NotFound(); // ถ้าไม่พบคำสั่งซื้อให้คืนค่า 404 Not Found

            order.Status = "Completed"; // เปลี่ยนสถานะคำสั่งซื้อเป็น "Completed"
            _context.SaveChanges(); // บันทึกการเปลี่ยนแปลงลงฐานข้อมูล
            return NoContent(); // คืนค่า 204 No Content (ไม่มีเนื้อหากลับไป)
        }

        // PUT: api/orders/{id}/cancel
        // เปลี่ยนสถานะคำสั่งซื้อให้เป็น "Canceled"
        [HttpPut("{id}/cancel")]
        public IActionResult CancelOrder(int id)
        {
            var order = _context.Orders.Find(id); // ค้นหาคำสั่งซื้อจาก id
            if (order == null) return NotFound(); // ถ้าไม่พบคำสั่งซื้อให้คืนค่า 404 Not Found

            order.Status = "Canceled"; // เปลี่ยนสถานะคำสั่งซื้อเป็น "Canceled"
            _context.SaveChanges(); // บันทึกการเปลี่ยนแปลงลงฐานข้อมูล
            return NoContent(); // คืนค่า 204 No Content (ไม่มีเนื้อหากลับไป)
        }
    }
}
