using API_ITI.Models;
using Microsoft.EntityFrameworkCore;

namespace API_ITI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employee { get; set; }
    }
}
