
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkSphere.Models;

[Table("Departments")]
public class Department
{
    [Key]
    public int DepartmentId { get; set; }

    [Required]
    [StringLength(100)]
    public string DepartmentName { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }
}
