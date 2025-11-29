using System.ComponentModel.DataAnnotations;
using UniversalIntegrationMiddleware.Models.Enums;

namespace UniversalIntegrationMiddleware.Models
{
    public class JobRun
    {
        public int Id { get; set; }

        public int FlowId { get; set; }
        public IntegrationFlow? Flow { get; set; }

        public DateTime StartedAt { get; set; } = DateTime.UtcNow;

        public DateTime? FinishedAt { get; set; }

        [Required]
        public JobStatus Status { get; set; } = JobStatus.Pending;

        public int TotalRecords { get; set; }

        public int SuccessCount { get; set; }

        public int FailureCount { get; set; }

        [MaxLength(2000)]
        public string ErrorMessage { get; set; } = string.Empty;

        // Navigation properties
        public ICollection<LogEntry> LogEntries { get; set; } = new List<LogEntry>();
    }
}
