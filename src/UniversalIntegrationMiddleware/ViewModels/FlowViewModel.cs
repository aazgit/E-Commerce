using System.ComponentModel.DataAnnotations;
using UniversalIntegrationMiddleware.Models;
using UniversalIntegrationMiddleware.Models.Enums;

namespace UniversalIntegrationMiddleware.ViewModels
{
    public class FlowDetailsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string SourceConnectionName { get; set; } = string.Empty;
        public string TargetConnectionName { get; set; } = string.Empty;
        public FlowType FlowType { get; set; }
        public string? MappingTemplateName { get; set; }
        public string? ScheduleDescription { get; set; }
        public bool IsEnabled { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<JobRun> RecentJobs { get; set; } = new List<JobRun>();
    }

    public class FlowListViewModel
    {
        public List<FlowListItemViewModel> Flows { get; set; } = new List<FlowListItemViewModel>();
    }

    public class FlowListItemViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public FlowType FlowType { get; set; }
        public string SourceConnectionName { get; set; } = string.Empty;
        public string TargetConnectionName { get; set; } = string.Empty;
        public string? ScheduleDescription { get; set; }
        public bool IsEnabled { get; set; }
        public JobStatus? LastJobStatus { get; set; }
    }

    public class EditFlowViewModel : CreateFlowViewModel
    {
        public int Id { get; set; }
    }
}
