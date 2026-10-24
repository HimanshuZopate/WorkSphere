
using Microsoft.EntityFrameworkCore;

namespace WorkSphere.Models;

public class WorkSphereDBContext : DbContext
{
    public WorkSphereDBContext(
        DbContextOptions<WorkSphereDBContext> options)
        : base(options)
    {
    }

    public DbSet<Employee> Employees => Set<Employee>();
    public DbSet<Department> Departments => Set<Department>();
}
