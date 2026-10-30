
using System.ComponentModel.DataAnnotations;

namespace WorkSphere.ViewModel;

public class EditEmployeeViewModel
{
    public int EmployeeId { get; set; }

    [Required]
    [StringLength(100)]
    public string FullName { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    [StringLength(255)]
    public string Email { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    public string JobTitle { get; set; } = string.Empty;

    [Range(0, 9999999999)]
    public decimal Salary { get; set; }

    [DataType(DataType.Date)]
    public DateTime HireDate { get; set; }

    [Required]
    [Range(1, int.MaxValue)]
    public int DepartmentId { get; set; }

    public bool IsActive { get; set; }
}
