using Microsoft.AspNetCore.Mvc;
using UniversalIntegrationMiddleware.Services;
using LogLevel = UniversalIntegrationMiddleware.Models.Enums.LogLevel;

namespace UniversalIntegrationMiddleware.Controllers
{
    public class LogsController : Controller
    {
        private readonly ILogService _logService;

        public LogsController(ILogService logService)
        {
            _logService = logService;
        }

        public async Task<IActionResult> Index(LogLevel? level = null, int? flowId = null, int? jobId = null, DateTime? startDate = null, DateTime? endDate = null)
        {
            var viewModel = await _logService.GetLogsAsync(User, level, flowId, jobId, startDate, endDate);
            return View(viewModel);
        }

        public async Task<IActionResult> Details(int id)
        {
            var log = await _logService.GetLogDetailsAsync(id, User);
            if (log == null) return NotFound();
            return View(log);
        }
    }
}
