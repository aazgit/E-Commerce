using UniversalIntegrationMiddleware.Models;
using LogLevel = UniversalIntegrationMiddleware.Models.Enums.LogLevel;

namespace UniversalIntegrationMiddleware.ViewModels
{
    public class LogListViewModel
    {
        public List<LogListItemViewModel> Logs { get; set; } = new List<LogListItemViewModel>();
        public LogLevel? LevelFilter { get; set; }
        public int? FlowIdFilter { get; set; }
        public int? JobIdFilter { get; set; }
        public DateTime? StartDateFilter { get; set; }
        public DateTime? EndDateFilter { get; set; }
    }

    public class LogListItemViewModel
    {
        public int Id { get; set; }
        public int? JobRunId { get; set; }
        public string? FlowName { get; set; }
        public LogLevel Level { get; set; }
        public string Message { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }

    public class LogDetailsViewModel
    {
        public int Id { get; set; }
        public int? JobRunId { get; set; }
        public string? FlowName { get; set; }
        public LogLevel Level { get; set; }
        public string Message { get; set; } = string.Empty;
        public string DetailsJson { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}
