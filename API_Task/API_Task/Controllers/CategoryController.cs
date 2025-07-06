using API_Task.DTO;
using API_Task.Models;
using API_Task.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_Task.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IGenericRepository<Category> _categoryRepository;
        public CategoryController(IGenericRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        [HttpGet("{Id:int}")]
        public async Task<ActionResult<CategoryWithListOfProductsDTO>> GetCategoryWithProducts (int Id) 
        {
            Category? category = await _categoryRepository.GetByIdWithIncludesAsync(c=>c.Id == Id, p=>p.Products);
            if (category == null)
            {
                return null;
            }
            CategoryWithListOfProductsDTO categoryWithProducts = new CategoryWithListOfProductsDTO
            {
                Id = category.Id,
                Name = category.Name,
                Products = category.Products.Select(p => new ProductDTO { Name = p.Name }).ToList()
            };
            return categoryWithProducts;
        }

    }
}
