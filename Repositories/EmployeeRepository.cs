
using Microsoft.EntityFrameworkCore;
using WorkSphere.Interfaces;
using WorkSphere.Models;

namespace WorkSphere.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly WorkSphereDBContext _context;

    public EmployeeRepository(WorkSphereDBContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Employee employee)
    {
        await _context.Employees.AddAsync(employee);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Employee>> GetAllAsync()
    {
        return await _context.Employees
            .AsNoTracking()
            .OrderBy(e => e.FullName)
            .ToListAsync();
    }
}
