using System.ComponentModel.DataAnnotations;
using UniversalIntegrationMiddleware.Models.Enums;

namespace UniversalIntegrationMiddleware.Models
{
    public class WebhookEndpoint
    {
        public int Id { get; set; }

        public int MerchantId { get; set; }
        public Merchant? Merchant { get; set; }

        [Required]
        public Platform Platform { get; set; }

        [Required]
        [MaxLength(500)]
        public string Url { get; set; } = string.Empty;

        [MaxLength(256)]
        public string Secret { get; set; } = string.Empty;

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public ICollection<WebhookEvent> WebhookEvents { get; set; } = new List<WebhookEvent>();
    }
}
