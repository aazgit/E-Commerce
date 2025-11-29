using System.ComponentModel.DataAnnotations;
using UniversalIntegrationMiddleware.Models.Enums;

namespace UniversalIntegrationMiddleware.Models
{
    public enum WebhookEventStatus
    {
        Pending = 1,
        Processing = 2,
        Completed = 3,
        Failed = 4
    }

    public class WebhookEvent
    {
        public int Id { get; set; }

        public int WebhookEndpointId { get; set; }
        public WebhookEndpoint? WebhookEndpoint { get; set; }

        [Required]
        [MaxLength(100)]
        public string EventType { get; set; } = string.Empty;

        public string Payload { get; set; } = string.Empty;

        [Required]
        public WebhookEventStatus Status { get; set; } = WebhookEventStatus.Pending;

        public int Retries { get; set; }

        public DateTime ReceivedAt { get; set; } = DateTime.UtcNow;

        public DateTime? ProcessedAt { get; set; }

        [MaxLength(2000)]
        public string ErrorMessage { get; set; } = string.Empty;
    }
}
