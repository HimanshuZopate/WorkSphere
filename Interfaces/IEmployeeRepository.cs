
using WorkSphere.Models;

namespace WorkSphere.Interfaces;

public interface IEmployeeRepository
{
    Task<List<Employee>> GetAllAsync();

    Task AddAsync(Employee employee);

    Task<List<Employee>> SearchAsync(
        string? name,
        int? departmentId,
        bool? isActive
        );


}
