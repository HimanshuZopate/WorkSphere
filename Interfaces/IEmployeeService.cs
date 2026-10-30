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

    Task<(List<Employee> Employees, int TotalCount)> GetPagedEmployeesAsync(
        string? name,
        int? departmentId,
        bool? isActive,
        int page,
        int pageSize
       );

    Task<Employee?> GetEmployeeByIdAsync(int id);

    Task<EditEmployeeViewModel?> GetEmployeeForEditAsync(int id);

    Task UpdateEmployeeAsync(int id, EditEmployeeViewModel model);

    Task<Employee?> GetEmployeeForDeleteAsync(int id);
    Task<bool> DeleteEmployeeAsync(int id);
 
}
