
using WorkSphere.Models;

namespace WorkSphere.Interfaces;

public interface IDepartmentRepository
{
    Task<List<Department>> GetAllAsync();
}
