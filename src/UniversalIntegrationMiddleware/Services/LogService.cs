using System.Security.Claims;
using UniversalIntegrationMiddleware.Models;
using UniversalIntegrationMiddleware.ViewModels;
using LogLevel = UniversalIntegrationMiddleware.Models.Enums.LogLevel;

namespace UniversalIntegrationMiddleware.Services
{
    public class LogService : ILogService
    {
        private static readonly List<LogEntry> _logs = new()
        {
            new LogEntry
            {
                Id = 1,
                JobRunId = 1,
                Level = LogLevel.Info,
                Message = "Job started: Shopify → D365 Order Sync",
                DetailsJson = "{}",
                CreatedAt = DateTime.UtcNow.AddHours(-2)
            },
            new LogEntry
            {
                Id = 2,
                JobRunId = 1,
                Level = LogLevel.Info,
                Message = "Successfully processed 150 orders",
                DetailsJson = @"{""processedCount"": 150}",
                CreatedAt = DateTime.UtcNow.AddHours(-2).AddMinutes(5)
            },
            new LogEntry
            {
                Id = 3,
                JobRunId = 2,
                Level = LogLevel.Warning,
                Message = "5 records failed validation",
                DetailsJson = @"{""failedRecords"": [""ORD-1001"", ""ORD-1002"", ""ORD-1003"", ""ORD-1004"", ""ORD-1005""]}",
                CreatedAt = DateTime.UtcNow.AddHours(-1).AddMinutes(3)
            },
            new LogEntry
            {
                Id = 4,
                JobRunId = 3,
                Level = LogLevel.Error,
                Message = "Connection timeout to Shopify API",
                DetailsJson = @"{""errorCode"": ""TIMEOUT"", ""retryCount"": 3, ""lastAttempt"": ""2024-01-15T10:30:00Z""}",
                CreatedAt = DateTime.UtcNow.AddMinutes(-25)
            },
            new LogEntry
            {
                Id = 5,
                JobRunId = null,
                Level = LogLevel.Info,
                Message = "System health check completed",
                DetailsJson = @"{""status"": ""healthy"", ""uptime"": ""99.9%""}",
                CreatedAt = DateTime.UtcNow.AddMinutes(-5)
            }
        };

        public Task<LogListViewModel> GetLogsAsync(ClaimsPrincipal user, LogLevel? level = null, int? flowId = null, int? jobId = null, DateTime? startDate = null, DateTime? endDate = null)
        {
            var query = _logs.AsEnumerable();

            if (level.HasValue)
                query = query.Where(l => l.Level == level.Value);

            if (jobId.HasValue)
                query = query.Where(l => l.JobRunId == jobId.Value);

            if (startDate.HasValue)
                query = query.Where(l => l.CreatedAt >= startDate.Value);

            if (endDate.HasValue)
                query = query.Where(l => l.CreatedAt <= endDate.Value);

            var viewModel = new LogListViewModel
            {
                Logs = query.OrderByDescending(l => l.CreatedAt).Select(l => new LogListItemViewModel
                {
                    Id = l.Id,
                    JobRunId = l.JobRunId,
                    FlowName = l.JobRunId.HasValue ? GetFlowNameForJob(l.JobRunId.Value) : null,
                    Level = l.Level,
                    Message = l.Message,
                    CreatedAt = l.CreatedAt
                }).ToList(),
                LevelFilter = level,
                JobIdFilter = jobId,
                StartDateFilter = startDate,
                EndDateFilter = endDate
            };
            return Task.FromResult(viewModel);
        }

        public Task<LogDetailsViewModel?> GetLogDetailsAsync(int id, ClaimsPrincipal user)
        {
            var log = _logs.FirstOrDefault(l => l.Id == id);
            if (log == null) return Task.FromResult<LogDetailsViewModel?>(null);

            var viewModel = new LogDetailsViewModel
            {
                Id = log.Id,
                JobRunId = log.JobRunId,
                FlowName = log.JobRunId.HasValue ? GetFlowNameForJob(log.JobRunId.Value) : null,
                Level = log.Level,
                Message = log.Message,
                DetailsJson = log.DetailsJson,
                CreatedAt = log.CreatedAt
            };
            return Task.FromResult<LogDetailsViewModel?>(viewModel);
        }

        private static string? GetFlowNameForJob(int jobId)
        {
            return jobId switch
            {
                1 => "Shopify → D365 Order Sync",
                2 => "D365 → Shopify Inventory Sync",
                3 => "Shopify → D365 Order Sync",
                4 => "D365 → Shopify Inventory Sync",
                _ => null
            };
        }
    }
}
