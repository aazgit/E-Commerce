using System.ComponentModel.DataAnnotations;

namespace UniversalIntegrationMiddleware.Models
{
    public class Schedule
    {
        public int Id { get; set; }

        public int MerchantId { get; set; }
        public Merchant? Merchant { get; set; }

        [MaxLength(100)]
        public string CronExpression { get; set; } = string.Empty;

        public int? IntervalMinutes { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public ICollection<IntegrationFlow> IntegrationFlows { get; set; } = new List<IntegrationFlow>();
    }
}
