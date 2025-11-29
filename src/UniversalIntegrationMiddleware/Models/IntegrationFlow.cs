using System.ComponentModel.DataAnnotations;
using UniversalIntegrationMiddleware.Models.Enums;

namespace UniversalIntegrationMiddleware.Models
{
    public class IntegrationFlow
    {
        public int Id { get; set; }

        public int MerchantId { get; set; }
        public Merchant? Merchant { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;

        public int SourceConnectionId { get; set; }
        public ChannelConnection? SourceConnection { get; set; }

        public int TargetConnectionId { get; set; }
        public ChannelConnection? TargetConnection { get; set; }

        [Required]
        public FlowType FlowType { get; set; }

        public int? MappingTemplateId { get; set; }
        public MappingTemplate? MappingTemplate { get; set; }

        public int? ScheduleId { get; set; }
        public Schedule? Schedule { get; set; }

        public bool IsEnabled { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public ICollection<JobRun> JobRuns { get; set; } = new List<JobRun>();
    }
}
