using System.ComponentModel.DataAnnotations;
using UniversalIntegrationMiddleware.Models.Enums;

namespace UniversalIntegrationMiddleware.Models
{
    public class ChannelConnection
    {
        public int Id { get; set; }

        public int MerchantId { get; set; }
        public Merchant? Merchant { get; set; }

        [Required]
        public Platform Platform { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(500)]
        public string BaseUrl { get; set; } = string.Empty;

        [MaxLength(1000)]
        public string AccessToken { get; set; } = string.Empty;

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? LastSyncedAt { get; set; }

        // Navigation properties
        public ICollection<IntegrationFlow> SourceFlows { get; set; } = new List<IntegrationFlow>();
        public ICollection<IntegrationFlow> TargetFlows { get; set; } = new List<IntegrationFlow>();
    }
}
