using System.ComponentModel.DataAnnotations;

namespace UniversalIntegrationMiddleware.ViewModels
{
    public class MappingViewModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        [Display(Name = "Template Name")]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "Source Schema (JSON)")]
        public string SourceSchema { get; set; } = string.Empty;

        [Display(Name = "Target Schema (JSON)")]
        public string TargetSchema { get; set; } = string.Empty;

        [Display(Name = "Transformation Rules (JSON)")]
        public string TransformationRules { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    public class MappingListViewModel
    {
        public List<MappingListItemViewModel> Mappings { get; set; } = new List<MappingListItemViewModel>();
    }

    public class MappingListItemViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int FlowsUsingCount { get; set; }
    }

    public class MappingPreviewViewModel
    {
        public int MappingId { get; set; }
        public string InputJson { get; set; } = string.Empty;
        public string OutputJson { get; set; } = string.Empty;
    }
}
