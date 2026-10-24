
using WorkSphere.Models;

namespace WorkSphere.Interfaces;

public interface IEmployeeRepository
{
    Task<List<Employee>> GetAllAsync();

    Task AddAsync(Employee employee);
}
