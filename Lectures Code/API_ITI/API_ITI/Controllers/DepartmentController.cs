using API_ITI.Data;
using API_ITI.DTO;
using API_ITI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_ITI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public DepartmentController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAllDepartments()
        {
            List<Department> departments = _context.Departments.ToList();
            return Ok(departments);
        }

        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetDepartmentById(int id)
        {
            Department department = _context.Departments.FirstOrDefault(d => d.Id == id);
            return Ok(department);
        }
        [HttpGet]
        [Route("{name:alpha}")]
        public IActionResult GetDepartmentByName(string name)
        {
            Department department = _context.Departments.FirstOrDefault(d => d.Name == name);
            return Ok(department);
        }
        [HttpGet("Count")]
        public ActionResult<List<DepartmentWithEmployeesCountDTO>> GetDeptartmentDetails()
        {
            List<Department> deptlist =
                _context.Departments.Include(d => d.Employees).ToList();

            List<DepartmentWithEmployeesCountDTO> deptListDto =
                new List<DepartmentWithEmployeesCountDTO>();

            foreach (Department item in deptlist)
            {
                DepartmentWithEmployeesCountDTO deptDto = new DepartmentWithEmployeesCountDTO() 
                {
                    Id = item.Id,
                    Name = item.Name,
                    Count = item.Employees.Count()
                };
                deptListDto.Add(deptDto);
            }
            return deptListDto;
            //return Ok(deptlistDto); IActionREsult
        }

        [HttpPost]
        public IActionResult CreateDepartment(Department department)
        {
            _context.Departments.Add(department);
            _context.SaveChanges();
            return CreatedAtAction("GetDepartmentById",new { id = department.Id},department);
        }

        [HttpPut("{id:int}")]
        public IActionResult UpdateDepartment(int id, Department departmentFromRequest) 
        {
            Department departmentFromDB = _context.Departments.FirstOrDefault(d => d.Id == id);
            if (departmentFromDB == null)
            {
                return NotFound();
            }
            departmentFromDB.Name = departmentFromRequest.Name;
            departmentFromDB.Manager = departmentFromRequest.Manager;
            _context.Departments.Update(departmentFromDB);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
