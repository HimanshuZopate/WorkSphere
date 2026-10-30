using WorkSphere.ViewModel;

namespace WorkSphere.Interfaces;

public interface IDashboardService
{
    Task<DashboardViewModel> GetDashboardDataAsync();
}
