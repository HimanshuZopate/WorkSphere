
using System.ComponentModel.DataAnnotations;

namespace WorkSphere.ViewModel;

public class CreateEmployeeViewModel
{
    [Required(ErrorMessage = "Full name is required.")]
    [StringLength(100, MinimumLength = 2)]
    [Display(Name = "Full Name")]
    public string FullName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Enter a valid email address.")]
    [StringLength(255)]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Job title is required.")]
    [StringLength(100)]
    [Display(Name = "Job Title")]
    public string JobTitle { get; set; } = string.Empty;

    [Range(0, 9999999999.99, ErrorMessage = "Salary must be non-negative.")]
    public decimal Salary { get; set; }

    [Required]
    [DataType(DataType.Date)]
    [Display(Name = "Hire Date")]
    public DateTime HireDate { get; set; } = DateTime.Today;
    [Range(1, int.MaxValue, ErrorMessage = "Select a department.")]
    [Display(Name = "Department")]
    public int DepartmentId { get; set; }
}
