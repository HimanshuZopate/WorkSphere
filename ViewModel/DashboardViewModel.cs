
namespace WorkSphere.ViewModel;

public class DashboardViewModel
{
    public int TotalEmployees { get; set; }

    public int ActiveEmployees { get; set; }

    public int InactiveEmployees { get; set; }

    public int TotalDepartments { get; set; }

    public List<DepartmentEmployeeCountViewModel> DepartmentStats { get; set; } = new();
}

public class DepartmentEmployeeCountViewModel
{
    public string DepartmentName { get; set; } = string.Empty;

    public int EmployeeCount { get; set; }
}
