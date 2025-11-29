using System.Security.Claims;
using UniversalIntegrationMiddleware.Models;
using UniversalIntegrationMiddleware.Models.Enums;
using UniversalIntegrationMiddleware.ViewModels;

namespace UniversalIntegrationMiddleware.Services
{
    public class JobService : IJobService
    {
        private static readonly List<JobRun> _jobs = new()
        {
            new JobRun
            {
                Id = 1,
                FlowId = 1,
                StartedAt = DateTime.UtcNow.AddHours(-2),
                FinishedAt = DateTime.UtcNow.AddHours(-2).AddMinutes(5),
                Status = JobStatus.Success,
                TotalRecords = 150,
                SuccessCount = 150,
                FailureCount = 0
            },
            new JobRun
            {
                Id = 2,
                FlowId = 2,
                StartedAt = DateTime.UtcNow.AddHours(-1),
                FinishedAt = DateTime.UtcNow.AddHours(-1).AddMinutes(3),
                Status = JobStatus.PartialSuccess,
                TotalRecords = 200,
                SuccessCount = 195,
                FailureCount = 5,
                ErrorMessage = "5 records failed due to validation errors"
            },
            new JobRun
            {
                Id = 3,
                FlowId = 1,
                StartedAt = DateTime.UtcNow.AddMinutes(-30),
                FinishedAt = DateTime.UtcNow.AddMinutes(-25),
                Status = JobStatus.Failed,
                TotalRecords = 0,
                SuccessCount = 0,
                FailureCount = 0,
                ErrorMessage = "Connection timeout to Shopify API"
            },
            new JobRun
            {
                Id = 4,
                FlowId = 2,
                StartedAt = DateTime.UtcNow.AddMinutes(-10),
                Status = JobStatus.Running,
                TotalRecords = 100,
                SuccessCount = 45,
                FailureCount = 0
            }
        };

        public Task<JobListViewModel> GetJobsAsync(ClaimsPrincipal user, JobStatus? status = null, int? flowId = null, DateTime? startDate = null, DateTime? endDate = null)
        {
            var query = _jobs.AsEnumerable();

            if (status.HasValue)
                query = query.Where(j => j.Status == status.Value);

            if (flowId.HasValue)
                query = query.Where(j => j.FlowId == flowId.Value);

            if (startDate.HasValue)
                query = query.Where(j => j.StartedAt >= startDate.Value);

            if (endDate.HasValue)
                query = query.Where(j => j.StartedAt <= endDate.Value);

            var viewModel = new JobListViewModel
            {
                Jobs = query.OrderByDescending(j => j.StartedAt).Select(j => new JobListItemViewModel
                {
                    Id = j.Id,
                    FlowId = j.FlowId,
                    FlowName = GetFlowName(j.FlowId),
                    StartedAt = j.StartedAt,
                    Duration = j.FinishedAt.HasValue ? j.FinishedAt.Value - j.StartedAt : null,
                    Status = j.Status,
                    SuccessCount = j.SuccessCount,
                    FailureCount = j.FailureCount
                }).ToList(),
                StatusFilter = status,
                FlowIdFilter = flowId,
                StartDateFilter = startDate,
                EndDateFilter = endDate
            };
            return Task.FromResult(viewModel);
        }

        public Task<JobDetailsViewModel?> GetJobDetailsAsync(int id, ClaimsPrincipal user)
        {
            var job = _jobs.FirstOrDefault(j => j.Id == id);
            if (job == null) return Task.FromResult<JobDetailsViewModel?>(null);

            var viewModel = new JobDetailsViewModel
            {
                Id = job.Id,
                FlowName = GetFlowName(job.FlowId),
                FlowType = FlowType.Orders,
                StartedAt = job.StartedAt,
                FinishedAt = job.FinishedAt,
                Status = job.Status,
                TotalRecords = job.TotalRecords,
                SuccessCount = job.SuccessCount,
                FailureCount = job.FailureCount,
                ErrorMessage = job.ErrorMessage,
                LogEntries = new List<LogEntry>()
            };
            return Task.FromResult<JobDetailsViewModel?>(viewModel);
        }

        public Task<int> TriggerJobAsync(int flowId, ClaimsPrincipal user)
        {
            var newId = _jobs.Count > 0 ? _jobs.Max(j => j.Id) + 1 : 1;
            var job = new JobRun
            {
                Id = newId,
                FlowId = flowId,
                StartedAt = DateTime.UtcNow,
                Status = JobStatus.Pending,
                TotalRecords = 0,
                SuccessCount = 0,
                FailureCount = 0
            };
            _jobs.Add(job);
            return Task.FromResult(newId);
        }

        public Task RetryFailedAsync(int jobId, ClaimsPrincipal user)
        {
            // In a real implementation, this would retry failed records
            return Task.CompletedTask;
        }

        private static string GetFlowName(int flowId)
        {
            return flowId switch
            {
                1 => "Shopify → D365 Order Sync",
                2 => "D365 → Shopify Inventory Sync",
                3 => "Amazon → D365 Products",
                _ => "Unknown Flow"
            };
        }
    }
}
