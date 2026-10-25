
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WorkSphere.Interfaces;
using WorkSphere.ViewModel;
using WorkSphere.ViewModel;

namespace WorkSphere.Controllers;

public class EmployeesController : Controller
{
    private readonly IEmployeeService _employeeService;
    private readonly IDepartmentRepository _departmentRepository;

    public EmployeesController(
        IEmployeeService employeeService,
        IDepartmentRepository departmentRepository)
    {
        _employeeService = employeeService;
        _departmentRepository = departmentRepository;
    }

    public async Task<IActionResult> Index(
        string? name,
        int? departmentId,
        bool? isActive)
    {
        var employees = await _employeeService.SearchEmployeesAsync(
            name,
            departmentId,
            isActive);

        await LoadDepartmentsAsync();

        ViewBag.Name = name;
        ViewBag.DepartmentId = departmentId;
        ViewBag.IsActive = isActive;

        return View(employees);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        await LoadDepartmentsAsync();
        return View(new CreateEmployeeViewModel());
    }



    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateEmployeeViewModel model)
    {
        Console.WriteLine($"Selected Department ID: {model.DepartmentId}");
        if (!ModelState.IsValid)
        {
            await LoadDepartmentsAsync();
            return View(model);
        }

        try
        {
            await _employeeService.CreateEmployeeAsync(model);
            TempData["SuccessMessage"] = "Employee created successfully!";
            return RedirectToAction(nameof(Index));
        }
        catch (ArgumentException ex)
        {
            ModelState.AddModelError(nameof(model.DepartmentId), ex.Message);
            await LoadDepartmentsAsync();
            return View(model);
        }
    }


    private async Task LoadDepartmentsAsync()
    {
        var departments = await _departmentRepository.GetAllAsync();

        ViewBag.Departments = new SelectList(
            departments ?? new List<WorkSphere.Models.Department>(),
            nameof(WorkSphere.Models.Department.DepartMentId),
            nameof(WorkSphere.Models.Department.DepartmentName)
        );
    }

}
