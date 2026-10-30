using Microsoft.EntityFrameworkCore;
using WorkSphere.Interfaces;
using WorkSphere.Models;
using WorkSphere.ViewModel;

namespace WorkSphere.Services; 

public class DashboardService : IDashboardService
{
    private readonly WorkSphereDBContext _context;

    public DashboardService(WorkSphereDBContext context)
    {
        _context = context;
    }

    public async Task<DashboardViewModel> GetDashboardDataAsync()
    {
        var dashboard = new DashboardViewModel
        {
            TotalEmployees = await _context.Employees.CountAsync(),
            ActiveEmployees = await _context.Employees.CountAsync(e => e.IsActive),

            InactiveEmployees = await _context.Employees.CountAsync(e => !e.IsActive),

            TotalDepartments = await _context.Departments.CountAsync(),
            DepartmentStats = await _context.Departments.Select(d => new DepartmentEmployeeCountViewModel
            {
                DepartmentName = d.DepartmentName,
                EmployeeCount = _context.Employees.Count(e => e.DepartmentId == d.DepartmentId)
            })
            .ToListAsync()
        };
        return dashboard;
    }
}
