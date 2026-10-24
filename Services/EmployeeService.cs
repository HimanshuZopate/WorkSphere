
using WorkSphere.Interfaces;
using WorkSphere.Models;
using WorkSphere.ViewModel;
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
            d => d.DepartMentId == model.DepartmentId);

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
}
