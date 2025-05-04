using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyWebAPI.Core;
using MyWebAPI.Data;

namespace MyWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly DataContext _context;
        public ProductController(DataContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<List<ProductEntity>>> Get()
        {
            return Ok(await _context.ProductEntities.ToListAsync());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductEntity>> Get(int id)
        {
            var product = await _context.ProductEntities.FindAsync(id);
            if(product == null)
                return BadRequest("Product id is not found.");
            return Ok(product);
        }
        [HttpPost]
        public async Task<ActionResult<List<ProductEntity>>> AddProduct(ProductEntity product)
        {
            _context.ProductEntities.Add(product);
            await _context.SaveChangesAsync();
            return Ok(await _context.ProductEntities.ToListAsync());
        }
        [HttpPut]
        public async Task<ActionResult<List<ProductEntity>>> UpdateProduct(ProductEntity request)
        {
            var product = await _context.ProductEntities.FindAsync(request.Id);
            if(product == null)
                return BadRequest("No product found to replace this id.");
            product.Name = request.Name;
            product.Price = request.Price;
            await _context.SaveChangesAsync();
            return Ok(await _context.ProductEntities.ToListAsync());
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<ProductEntity>>> DeleteProduct(int id)
        {
            var product = await _context.ProductEntities.FindAsync(id);
            if(product == null)
                return NotFound("No product found to delete");
            _context.ProductEntities.Remove(product);
            await _context.SaveChangesAsync();
            return Ok(await _context.ProductEntities.ToListAsync());
        }
    }
}
