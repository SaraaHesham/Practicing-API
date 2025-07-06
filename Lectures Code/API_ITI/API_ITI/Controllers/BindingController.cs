using API_ITI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_ITI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BindingController : ControllerBase
    {
        // Bining of primitive types from route parameters
        [HttpGet("{id:int}/{name:alpha}")]
        public IActionResult TestPrimitive(int id, string name)
        {
            return Ok();
        }

        // Binding of complex types from request body and primitive type name from query string 
        [HttpPost]
        public IActionResult TestObject(Department department, string? name) 
        {
            return Ok();
        }

        // Binding of complex types from route values "Route Values must be of the same name of the complex type attributes" 
        [HttpGet("{Id:int}/{Name:alpha}/{Manager:alpha}")]
        public IActionResult TestCustomBind([FromRoute]Department department, string? name)
        {
            return Ok();
        }
    }
}
