using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using UniversalIntegrationMiddleware.Models.Enums;

namespace UniversalIntegrationMiddleware.ViewModels
{
    public class CreateFlowViewModel
    {
        [Required]
        [MaxLength(200)]
        [Display(Name = "Flow Name")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Source Connection")]
        public int SourceConnectionId { get; set; }

        [Required]
        [Display(Name = "Target Connection")]
        public int TargetConnectionId { get; set; }

        [Required]
        [Display(Name = "Flow Type")]
        public FlowType FlowType { get; set; }

        [Display(Name = "Mapping Template")]
        public int? MappingTemplateId { get; set; }

        [Display(Name = "Schedule")]
        public int? ScheduleId { get; set; }

        [Display(Name = "Enabled")]
        public bool IsEnabled { get; set; } = true;

        // Dropdown lists
        public SelectList? SourceConnections { get; set; }
        public SelectList? TargetConnections { get; set; }
        public SelectList? FlowTypes { get; set; }
        public SelectList? MappingTemplates { get; set; }
        public SelectList? Schedules { get; set; }
    }
}
