using Microsoft.AspNetCore.Mvc;
using BelleCroissantAPI.Data;
using BelleCroissantAPI.Model;

namespace BelleCroissantAPI.Controllers
{
    // กำหนดเส้นทางของ API สำหรับการจัดการข้อมูลสินค้า
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        // Constructor สำหรับการเชื่อมต่อกับฐานข้อมูล
        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/products
        // ดึงข้อมูลสินค้าทั้งหมดจากฐานข้อมูล
        [HttpGet]
        public IActionResult GetProducts()
        {
            var products = _context.Products.ToList(); // ดึงข้อมูลสินค้าทั้งหมด
            return Ok(products); // คืนค่าผลลัพธ์ในรูปแบบ JSON พร้อมสถานะ 200 OK
        }

        // GET: api/products/{id}
        // ดึงข้อมูลสินค้าจาก id ที่ระบุ
        [HttpGet("{id}")]
        public IActionResult GetProduct(int id)
        {
            var product = _context.Products.Find(id); // ค้นหาสินค้าจาก id
            if (product == null) return NotFound(); // ถ้าไม่พบสินค้าให้คืนค่า 404 Not Found
            return Ok(product); // คืนค่าสินค้าในรูปแบบ JSON พร้อมสถานะ 200 OK
        }

        // POST: api/products
        // เพิ่มสินค้าใหม่
        [HttpPost]
        public IActionResult AddProduct([FromBody] Product product)
        {
            _context.Products.Add(product); // เพิ่มสินค้าใหม่ลงฐานข้อมูล
            _context.SaveChanges(); // บันทึกการเพิ่มสินค้า
            return CreatedAtAction(nameof(GetProduct), new { id = product.ProductId }, product); // คืนค่าการสร้างใหม่พร้อมลิงก์ไปยังสินค้า
        }

        // PUT: api/products/{id}
        // อัปเดตข้อมูลสินค้าที่มี id
        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id, [FromBody] Product product)
        {
            var existingProduct = _context.Products.Find(id); // ค้นหาสินค้าเดิมจาก id
            if (existingProduct == null) return NotFound(); // ถ้าไม่พบสินค้าให้คืนค่า 404 Not Found

            // อัปเดตข้อมูลสินค้า
            existingProduct.Name = product.Name;
            existingProduct.Description = product.Description;
            existingProduct.Price = product.Price;

            _context.SaveChanges(); // บันทึกการเปลี่ยนแปลง
            return NoContent(); // คืนค่า 204 No Content (ไม่มีเนื้อหากลับไป)
        }

        // DELETE: api/products/{id}
        // ลบข้อมูลสินค้าที่มี id
        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var product = _context.Products.Find(id); // ค้นหาสินค้าเดิมจาก id
            if (product == null) return NotFound(); // ถ้าไม่พบสินค้าให้คืนค่า 404 Not Found

            _context.Products.Remove(product); // ลบสินค้าจากฐานข้อมูล
            _context.SaveChanges(); // บันทึกการลบสินค้า
            return NoContent(); // คืนค่า 204 No Content (ไม่มีเนื้อหากลับไป)
        }
    }
}
