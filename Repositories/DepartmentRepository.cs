
using Microsoft.EntityFrameworkCore;
using WorkSphere.Interfaces;
using WorkSphere.Models;

namespace WorkSphere.Repositories;

public class DepartmentRepository : IDepartmentRepository
{
    private readonly WorkSphereDBContext _context;

    public DepartmentRepository(WorkSphereDBContext context)
    {
        _context = context;
    }

    public async Task<List<Department>> GetAllAsync()
    {
        return await _context.Departments
            .AsNoTracking()
            .OrderBy(d => d.DepartmentName)
            .ToListAsync();
    }
}
