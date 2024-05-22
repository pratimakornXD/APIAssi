using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.DataAccess;
using WebApplication1.Models;

namespace TestWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ApplicationContext _context;
        public ProductController(ApplicationContext context)
        {
            _context = context;
        }
        [HttpGet("Products")]
        public IActionResult Get()
        {
            var products = _context.Products.ToList();
            return Ok(products);
        }
        [HttpGet("Products/{id}")]
        public IActionResult GetProduct([FromRoute] int id)
        {
            var products = _context.Products.FirstOrDefault(x => x.Id == id);
            if (products == null)
            {
                return NotFound();
            }
            return Ok(products);
        }
        [HttpPost("Products")]
        public IActionResult Save([FromBody] Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
            return Ok();
        }
        [HttpPut("Products")]
        public IActionResult Update([FromBody] Product product)
        {
            var result = _context.Products.AsNoTracking().FirstOrDefault(x => x.Id == product.Id);
            if (result == null)
            {
                return NotFound();
            }
            _context.Products.Update(product);
            _context.SaveChanges();
            return Ok();
        }
        [HttpDelete("Products")]
        public IActionResult Delete([FromQuery] int id)
        {
            var deleteProduct = _context.Products.FirstOrDefault(x => x.Id == id);
            if(deleteProduct == null)
            {
                return NotFound();
            }
            _context.Products.Remove(deleteProduct);
            _context.SaveChanges();
            return Ok();
        }
    }
}
