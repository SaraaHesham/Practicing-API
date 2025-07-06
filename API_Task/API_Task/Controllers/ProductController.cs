using API_Task.Models;
using API_Task.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_Task.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IGenericRepository<Product> _productRepository;
        public ProductController(IGenericRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllProductsAsync()
        {
            IEnumerable<Product> products = await _productRepository.GetAllAsync();
            return Ok(products);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetProductByIdAsync(int id)
        {
            Product product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
        [HttpPost]
        public async Task<IActionResult> AddProductAsync(Product product) 
        {
            await _productRepository.AddAsync(product);
            await _productRepository.SaveAsync();
            return CreatedAtAction("GetProductById", new { id = product.Id}, product);
        }
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateProductAsync(int id, Product product) 
        {
            Product? exsitingProduct = await _productRepository.GetByIdAsync(id);
            if (exsitingProduct != null)
            {
                exsitingProduct.Name = product.Name;
                exsitingProduct.Price = product.Price;
                exsitingProduct.Description = product.Description;
                exsitingProduct.StockQuantity = product.StockQuantity;
                _productRepository.Update(exsitingProduct);
                await _productRepository.SaveAsync();
                return NoContent();
            }
            return NotFound();
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteProductAsync(int id)
        {
            Product? exsitingProduct = await _productRepository.GetByIdAsync(id);
            if (exsitingProduct != null) 
            {
                _productRepository.Delete(exsitingProduct);
                await _productRepository.SaveAsync();
                return NoContent();
            }
            return NotFound();
        }
    }
}
