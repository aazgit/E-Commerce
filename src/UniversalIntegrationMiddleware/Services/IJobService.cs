using System.Security.Claims;
using UniversalIntegrationMiddleware.Models;
using UniversalIntegrationMiddleware.Models.Enums;
using UniversalIntegrationMiddleware.ViewModels;

namespace UniversalIntegrationMiddleware.Services
{
    public interface IJobService
    {
        Task<JobListViewModel> GetJobsAsync(ClaimsPrincipal user, JobStatus? status = null, int? flowId = null, DateTime? startDate = null, DateTime? endDate = null);
        Task<JobDetailsViewModel?> GetJobDetailsAsync(int id, ClaimsPrincipal user);
        Task<int> TriggerJobAsync(int flowId, ClaimsPrincipal user);
        Task RetryFailedAsync(int jobId, ClaimsPrincipal user);
    }
}
