using Microsoft.AspNetCore.Mvc;
using UniversalIntegrationMiddleware.Models;
using UniversalIntegrationMiddleware.Models.Enums;

namespace UniversalIntegrationMiddleware.Controllers
{
    [ApiController]
    [Route("api/webhooks")]
    public class WebhookController : ControllerBase
    {
        private readonly ILogger<WebhookController> _logger;

        public WebhookController(ILogger<WebhookController> logger)
        {
            _logger = logger;
        }

        [HttpPost("{platform}")]
        public async Task<IActionResult> Receive(string platform, [FromBody] object payload)
        {
            if (!Enum.TryParse<Platform>(platform, true, out var platformEnum))
            {
                _logger.LogWarning("Received webhook for unknown platform: {Platform}", platform);
                return BadRequest(new { error = "Unknown platform" });
            }

            _logger.LogInformation("Received webhook from {Platform}", platformEnum);

            // In a real implementation:
            // 1. Validate webhook signature/secret
            // 2. Save WebhookEvent to database
            // 3. Enqueue processing job

            var webhookEvent = new WebhookEvent
            {
                EventType = GetEventType(platformEnum, payload),
                Payload = System.Text.Json.JsonSerializer.Serialize(payload),
                Status = WebhookEventStatus.Pending,
                ReceivedAt = DateTime.UtcNow
            };

            // TODO: Save to database and enqueue processing

            return Ok(new { message = "Webhook received", eventId = Guid.NewGuid() });
        }

        [HttpGet("health")]
        public IActionResult Health()
        {
            return Ok(new { status = "healthy", timestamp = DateTime.UtcNow });
        }

        private static string GetEventType(Platform platform, object payload)
        {
            // In a real implementation, this would parse the payload to determine the event type
            return platform switch
            {
                Platform.Shopify => "shopify/order/created",
                Platform.Amazon => "amazon/order/created",
                Platform.VTEX => "vtex/order/created",
                _ => "unknown"
            };
        }
    }
}
