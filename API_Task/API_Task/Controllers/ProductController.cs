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
        public IActionResult GetAllProducts()
        {
            IEnumerable<Product> products = _productRepository.GetAll();
            return Ok(products);
        }
        [HttpGet("{id:int}")]
        public IActionResult GetProductById(int id)
        {
            Product product = _productRepository.GetById(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
        [HttpPost]
        public IActionResult AddProduct(Product product) 
        {
            _productRepository.Add(product);
            _productRepository.Save();
            return CreatedAtAction("GetProductById", new { id = product.Id}, product);
        }
        [HttpPut("{id:int}")]
        public IActionResult UpdateProduct(int id, Product product) 
        {
            Product? exsitingProduct = _productRepository.GetById(id);
            if (exsitingProduct != null)
            {
                exsitingProduct.Name = product.Name;
                exsitingProduct.Price = product.Price;
                exsitingProduct.Description = product.Description;
                exsitingProduct.StockQuantity = product.StockQuantity;
                _productRepository.Update(exsitingProduct);
                _productRepository.Save();
                return NoContent();
            }
            return NotFound();
        }
        [HttpDelete("{id:int}")]
        public IActionResult DeleteProduct(int id)
        {
            Product? exsitingProduct = _productRepository.GetById(id);
            if (exsitingProduct != null) 
            {
                _productRepository.Delete(exsitingProduct);
                _productRepository.Save();
                return NoContent();
            }
            return NotFound();
        }
    }
}
