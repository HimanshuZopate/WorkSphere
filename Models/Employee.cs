using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WorkSphere.Models;

[Table("Employees")]
public class Employee
{

    public Department? Department { get; set; }
    [Key]
    public int EmployeeId { get; set; }

    [Required]
    [StringLength(100)]
    public string FullName { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    [StringLength(225)]
    public string Email { set; get; } = string.Empty;

    [Required]
    [StringLength(100)]
    public string JobTitle { get; set; } = string.Empty;

    [Column(TypeName = "decimal(12,2)")]
    public decimal Salary { get; set; }
    public DateTime HireDate { get; set; }
    public bool IsActive { get; set; }
    public int DepartmentId { get; set; }
}