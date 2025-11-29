using Microsoft.AspNetCore.Mvc;
using UniversalIntegrationMiddleware.Services;

namespace UniversalIntegrationMiddleware.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IDashboardService _dashboardService;

        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = await _dashboardService.GetDashboardDataAsync(User);
            return View(viewModel);
        }
    }
}
