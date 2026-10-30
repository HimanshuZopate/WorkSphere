
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WorkSphere.Interfaces;
using WorkSphere.Models;
using WorkSphere.ViewModel;
using System.Linq;
using System;

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
        bool? isActive,
        int page = 1)
    {
        const int pageSize = 10;

        var result = await _employeeService.GetPagedEmployeesAsync(
            name,
            departmentId,
            isActive,
            page,
            pageSize);

        await LoadDepartmentsAsync(departmentId);

        ViewBag.Name = name;
        ViewBag.DepartmentId = departmentId;
        ViewBag.IsActive = isActive;
        ViewBag.CurrentPage = page;
        ViewBag.PageSize = pageSize;
        ViewBag.TotalCount = result.TotalCount;
        ViewBag.TotalPages = (int)Math.Ceiling(
            result.TotalCount / (double)pageSize);

        return View(result.Employees);
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
            await LoadDepartmentsAsync(model.DepartmentId);
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
            await LoadDepartmentsAsync(model.DepartmentId);
            return View(model);
        }
    }


    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var employee = await _employeeService.GetEmployeeByIdAsync(id);
        if (employee== null)
        {
            return NotFound();
        }
        return View(employee);
    }


    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var model = await _employeeService.GetEmployeeForEditAsync(id);

        if (model == null)
        {
            return NotFound();
        }

        await LoadDepartmentsAsync(model.DepartmentId);

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, EditEmployeeViewModel model)
    {
        if (id != model.EmployeeId)
        {
            return BadRequest();
        }

        if (!ModelState.IsValid)
        {
            await LoadDepartmentsAsync(model.DepartmentId);
            return View(model);
        }

        try
        {
            await _employeeService.UpdateEmployeeAsync(id, model);

            TempData["SuccessMessage"] = "Employee updated successfully!";

            return RedirectToAction(nameof(Index));
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch (ArgumentException ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);

            await LoadDepartmentsAsync(model.DepartmentId);

            return View(model);
        }
    }



    private async Task LoadDepartmentsAsync(int? selectedDepartmentId = null)
    {
        var departments = await _departmentRepository.GetAllAsync();

        if (departments == null)
        {
            throw new InvalidOperationException(
                "Department repository returned a null list.");
        }

        var validDepartments = departments
            .Where(d => d != null)
            .ToList();

        ViewBag.Departments = new SelectList(
            validDepartments,
            nameof(Department.DepartmentId),
            nameof(Department.DepartmentName),
            selectedDepartmentId);
    }


    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var employee = await _employeeService.GetEmployeeForDeleteAsync(id);

        if(employee == null)
        {
            return NotFound();
        }

        return View(employee);
    }
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var deleted = await _employeeService.DeleteEmployeeAsync(id);

        if (!deleted)
        {
            return NotFound();
        }

        TempData["SuccessMessage"] = "Employee deleted successfully!";

        return RedirectToAction(nameof(Index));
    }


}
