using System.ComponentModel.DataAnnotations;

namespace UniversalIntegrationMiddleware.Models
{
    public class Merchant
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [MaxLength(256)]
        public string ContactEmail { get; set; } = string.Empty;

        [MaxLength(512)]
        public string ApiKeyHash { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public ICollection<ChannelConnection> ChannelConnections { get; set; } = new List<ChannelConnection>();
        public ICollection<IntegrationFlow> IntegrationFlows { get; set; } = new List<IntegrationFlow>();
        public ICollection<MappingTemplate> MappingTemplates { get; set; } = new List<MappingTemplate>();
        public ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();
    }
}
