using WorkSphere.ViewModel;
using WorkSphere.Models;

namespace WorkSphere.Interfaces;

public interface IEmployeeService
{

    Task<List<Employee>> GetAllEmployeesAsync();

    Task CreateEmployeeAsync(CreateEmployeeViewModel model);

    Task<List<Employee>> SearchEmployeesAsync(
        string? name,
        int? departmentId,
        bool? isActive);
}
