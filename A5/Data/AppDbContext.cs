using A5.Models;
using Microsoft.EntityFrameworkCore;

namespace A5.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {

        }

        public DbSet<Organisation> Organisations{ get; set; }
        public DbSet<Department> Departments{ get; set; }
        public DbSet<Designation> Designations{ get; set; }
        
    }
}