using BelleCroissantAPI.Data;
using Microsoft.AspNetCore.Mvc;

namespace BelleCroissantAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class XnxxController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        // Constructor เพื่อรับ ApplicationDbContext มาใช้ในการเชื่อมต่อกับฐานข้อมูล
        public XnxxController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetXnxx()
        {
            var customers = _context.Customers.ToList();

            return Ok(customers); // ส่งข้อมูลที่แปลงแล้วกลับไป
        }
    }
}
