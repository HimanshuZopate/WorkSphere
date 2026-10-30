
using Microsoft.AspNetCore.Mvc;
using WorkSphere.Interfaces;

namespace WorkSphere.Controllers;

public class DashboardController : Controller
{
    private readonly IDashboardService _dashboardService;

    public DashboardController(IDashboardService dashboardService)
    {
        _dashboardService = dashboardService;
    }

    public async Task<IActionResult> Index()
    {
        var dashboard = await _dashboardService.GetDashboardDataAsync();

        return View(dashboard);
    }
}
