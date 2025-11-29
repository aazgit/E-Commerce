using UniversalIntegrationMiddleware.Models;
using UniversalIntegrationMiddleware.Models.Enums;

namespace UniversalIntegrationMiddleware.ViewModels
{
    public class JobDetailsViewModel
    {
        public int Id { get; set; }
        public string FlowName { get; set; } = string.Empty;
        public FlowType FlowType { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime? FinishedAt { get; set; }
        public TimeSpan? Duration => FinishedAt.HasValue ? FinishedAt.Value - StartedAt : null;
        public JobStatus Status { get; set; }
        public int TotalRecords { get; set; }
        public int SuccessCount { get; set; }
        public int FailureCount { get; set; }
        public string ErrorMessage { get; set; } = string.Empty;
        public List<LogEntry> LogEntries { get; set; } = new List<LogEntry>();
    }

    public class JobListViewModel
    {
        public List<JobListItemViewModel> Jobs { get; set; } = new List<JobListItemViewModel>();
        public JobStatus? StatusFilter { get; set; }
        public int? FlowIdFilter { get; set; }
        public DateTime? StartDateFilter { get; set; }
        public DateTime? EndDateFilter { get; set; }
    }

    public class JobListItemViewModel
    {
        public int Id { get; set; }
        public int FlowId { get; set; }
        public string FlowName { get; set; } = string.Empty;
        public DateTime StartedAt { get; set; }
        public TimeSpan? Duration { get; set; }
        public JobStatus Status { get; set; }
        public int SuccessCount { get; set; }
        public int FailureCount { get; set; }
    }
}
