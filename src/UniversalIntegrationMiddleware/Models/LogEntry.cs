using System.ComponentModel.DataAnnotations;
using UniversalIntegrationMiddleware.Models.Enums;
using LogLevel = UniversalIntegrationMiddleware.Models.Enums.LogLevel;

namespace UniversalIntegrationMiddleware.Models
{
    public class LogEntry
    {
        public int Id { get; set; }

        public int? JobRunId { get; set; }
        public JobRun? JobRun { get; set; }

        [Required]
        public LogLevel Level { get; set; }

        [Required]
        [MaxLength(2000)]
        public string Message { get; set; } = string.Empty;

        public string DetailsJson { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
