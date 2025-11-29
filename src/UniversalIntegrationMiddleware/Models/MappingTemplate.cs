using System.ComponentModel.DataAnnotations;

namespace UniversalIntegrationMiddleware.Models
{
    public class MappingTemplate
    {
        public int Id { get; set; }

        public int MerchantId { get; set; }
        public Merchant? Merchant { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;

        public string SourceSchema { get; set; } = string.Empty;

        public string TargetSchema { get; set; } = string.Empty;

        public string TransformationRules { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public ICollection<IntegrationFlow> IntegrationFlows { get; set; } = new List<IntegrationFlow>();
    }
}
