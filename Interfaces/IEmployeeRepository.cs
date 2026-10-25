
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


    Task<(List<Employee> Employees, int TotalCount)> GetPagedAsync(
        string? name,
        int? departmentId,
        bool? isActive,
        int page,
        int pageSize);





}
