
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

    public async Task<List<Employee>> SearchAsync(
        string? name,
        int? departmentId,
        bool? isActive
        )
    {
        var query = _context.Employees
            .AsNoTracking()
            .Include(e => e.Department)
            .AsQueryable();
        if (!string.IsNullOrWhiteSpace(name))
        {
            query = query.Where(e => e.FullName.Contains(name.Trim()));
        }

        if (departmentId.HasValue)
        {
            query = query.Where(e => e.DepartmentId == departmentId.Value);
        }

        if (isActive.HasValue)
        {
            query = query.Where(e => e.IsActive == isActive.Value);
        }

        return await query
            .OrderBy(e => e.FullName)
            .ToListAsync();

    }

    public async Task<List<Employee>> GetAllAsync()
    {
        return await _context.Employees
    .AsNoTracking()
    .Include(e => e.Department)
    .OrderBy(e => e.FullName)
    .ToListAsync();
    }


    public async Task<(List<Employee> Employees, int TotalCount)> GetPagedAsync(
        string? name,
        int? departmentId,
        bool? isActive,
        int page,
        int pageSize)
    {
        var query = _context.Employees
            .AsNoTracking()
            .Include(e => e.Department)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(name))
        {
            query = query.Where(e =>
                e.FullName.Contains(name.Trim()));
        }

        if (departmentId.HasValue)
        {
            query = query.Where(e =>
                e.DepartmentId == departmentId.Value);
        }

        if (isActive.HasValue)
        {
            query = query.Where(e =>
                e.IsActive == isActive.Value);
        }

        var totalCount = await query.CountAsync();

        var employees = await query
            .OrderBy(e => e.FullName)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (employees, totalCount);
    }

}
