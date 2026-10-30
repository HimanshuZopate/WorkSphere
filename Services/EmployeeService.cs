
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

    public async Task<Employee?> GetEmployeeByIdAsync(int id)
    {
        if (id <= 0)
        {
            return null;
        }
        return await _repository.GetByIdAsync(id);
    }


    public async Task<EditEmployeeViewModel?> GetEmployeeForEditAsync(int id)
    {
        if (id <= 0)
        {
            return null;
        }

        var employee = await _repository.GetByIdAsync(id);

        if (employee == null)
        {
            return null;
        }

        return new EditEmployeeViewModel
        {
            EmployeeId = employee.EmployeeId,
            FullName = employee.FullName,
            Email = employee.Email,
            JobTitle = employee.JobTitle,
            Salary = employee.Salary,
            HireDate = employee.HireDate,
            DepartmentId = employee.DepartmentId,
            IsActive = employee.IsActive
        };
    }

    public async Task UpdateEmployeeAsync(int id, EditEmployeeViewModel model)
    {
        if (id <= 0 || id != model.EmployeeId)
        {
            throw new ArgumentException("Invalid employee ID.");
        }

        var employee = await _repository.GetByIdAsync(id);

        if (employee == null)
        {
            throw new KeyNotFoundException("Employee not found.");
        }

        var departments = await _departmentRepository.GetAllAsync();

        bool departmentExists = departments.Any(
            d => d.DepartmentId == model.DepartmentId);

        if (!departmentExists)
        {
            throw new ArgumentException(
                "The selected department does not exist.");
        }

        employee.FullName = model.FullName.Trim();
        employee.Email = model.Email.Trim();
        employee.JobTitle = model.JobTitle.Trim();
        employee.Salary = model.Salary;
        employee.HireDate = model.HireDate;
        employee.DepartmentId = model.DepartmentId;
        employee.IsActive = model.IsActive;

        await _repository.UpdateAsync(employee);
    }

    public async Task<Employee?> GetEmployeeForDeleteAsync(int id)
    {
        if(id <= 0)
        {
            return null;
        }

        return await _repository.GetByIdAsync(id);
    }

    public async Task<bool> DeleteEmployeeAsync(int id)
    {
        if(id <= 0)
        {
            return false;
        }

        return await _repository.DeleteAsync(id);
    }

}
