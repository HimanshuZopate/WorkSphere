using WorkSphere.ViewModel;
using WorkSphere.Models;

namespace WorkSphere.Interfaces;

public interface IEmployeeService
{

    Task<List<Employee>> GetAllEmployeesAsync();

    Task CreateEmployeeAsync(CreateEmployeeViewModel model);
}
