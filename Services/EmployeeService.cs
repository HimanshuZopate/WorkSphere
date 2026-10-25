
using WorkSphere.Interfaces;
using WorkSphere.Models;
using WorkSphere.ViewModel;

namespace WorkSphere.Services;

public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository _repository;
    private readonly IDepartmentRepository _departmentRepository;

    public EmployeeService(
        IEmployeeRepository repository,
        IDepartmentRepository departmentRepository)
    {
        _repository = repository;
        _departmentRepository = departmentRepository;
    }

    public async Task<List<Employee>> GetAllEmployeesAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task CreateEmployeeAsync(
        CreateEmployeeViewModel model)
    {
        var departments = await _departmentRepository.GetAllAsync();

        bool departmentExists = departments.Any(
            d => d.DepartmentId == model.DepartmentId);

        if (!departmentExists)
        {
            throw new ArgumentException(
                "The selected department does not exist.");
        }

        var employee = new Employee
        {
            FullName = model.FullName.Trim(),
            Email = model.Email.Trim(),
            JobTitle = model.JobTitle.Trim(),
            Salary = model.Salary,
            HireDate = model.HireDate,
            DepartmentId = model.DepartmentId,
            IsActive = true
        };

        await _repository.AddAsync(employee);
    }
    public async Task<List<Employee>> SearchEmployeesAsync(
        string? name,
        int? departmentId,
        bool? isActive
        )
    {
        return await _repository.SearchAsync(
            name,
            departmentId,
            isActive);
    }

    public async Task<(List<Employee> Employees, int TotalCount)> GetPagedEmployeesAsync(
        string? name,
        int? departmentId,
        bool? isActive,
        int page,
        int pageSize)
    {
        page = Math.Max(1, page);
        pageSize = Math.Clamp(pageSize, 1, 100);

        return await _repository.GetPagedAsync(
            name,
            departmentId,
            isActive,
            page,
            pageSize);
    }
}
