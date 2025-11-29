using Microsoft.AspNetCore.Mvc;
using UniversalIntegrationMiddleware.Models.Enums;
using UniversalIntegrationMiddleware.Services;

namespace UniversalIntegrationMiddleware.Controllers
{
    public class JobController : Controller
    {
        private readonly IJobService _jobService;

        public JobController(IJobService jobService)
        {
            _jobService = jobService;
        }

        public async Task<IActionResult> Index(JobStatus? status = null, int? flowId = null, DateTime? startDate = null, DateTime? endDate = null)
        {
            var viewModel = await _jobService.GetJobsAsync(User, status, flowId, startDate, endDate);
            return View(viewModel);
        }

        public async Task<IActionResult> Details(int id)
        {
            var job = await _jobService.GetJobDetailsAsync(id, User);
            if (job == null) return NotFound();
            return View(job);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RetryFailed(int id)
        {
            await _jobService.RetryFailedAsync(id, User);
            return RedirectToAction(nameof(Details), new { id });
        }
    }
}
