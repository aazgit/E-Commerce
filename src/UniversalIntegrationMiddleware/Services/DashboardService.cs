using System.Security.Claims;
using UniversalIntegrationMiddleware.Models;
using UniversalIntegrationMiddleware.Models.Enums;
using UniversalIntegrationMiddleware.ViewModels;

namespace UniversalIntegrationMiddleware.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly IConnectorService _connectorService;
        private readonly IFlowService _flowService;
        private readonly IJobService _jobService;

        public DashboardService(IConnectorService connectorService, IFlowService flowService, IJobService jobService)
        {
            _connectorService = connectorService;
            _flowService = flowService;
            _jobService = jobService;
        }

        public async Task<DashboardViewModel> GetDashboardDataAsync(ClaimsPrincipal user)
        {
            var connectors = await _connectorService.GetConnectorsForMerchantAsync(user);
            var flows = await _flowService.GetFlowsForMerchantAsync(user);
            var jobs = await _jobService.GetJobsAsync(user);

            var today = DateTime.UtcNow.Date;
            var todayJobs = jobs.Jobs.Where(j => j.StartedAt >= today).ToList();

            var viewModel = new DashboardViewModel
            {
                ActiveConnectorsCount = connectors.Connectors.Count(c => c.IsActive),
                ActiveFlowsCount = flows.Flows.Count(f => f.IsEnabled),
                TotalJobsToday = todayJobs.Count,
                FailedJobsCount = todayJobs.Count(j => j.Status == JobStatus.Failed),
                RecentJobs = jobs.Jobs.Take(10).Select(j => new JobRun
                {
                    Id = j.Id,
                    FlowId = j.FlowId,
                    StartedAt = j.StartedAt,
                    FinishedAt = j.Duration.HasValue ? j.StartedAt + j.Duration.Value : null,
                    Status = j.Status,
                    SuccessCount = j.SuccessCount,
                    FailureCount = j.FailureCount
                }).ToList(),
                RecentConnectors = connectors.Connectors.Take(5).Select(c => new ChannelConnection
                {
                    Id = c.Id,
                    Name = c.Name,
                    Platform = c.Platform,
                    IsActive = c.IsActive,
                    LastSyncedAt = c.LastSyncedAt,
                    CreatedAt = c.CreatedAt
                }).ToList()
            };

            return viewModel;
        }
    }
}
