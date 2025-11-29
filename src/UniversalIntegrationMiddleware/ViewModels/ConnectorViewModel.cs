using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using UniversalIntegrationMiddleware.Models.Enums;

namespace UniversalIntegrationMiddleware.ViewModels
{
    public class ConnectorViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Platform")]
        public Platform Platform { get; set; }

        [Required]
        [MaxLength(200)]
        [Display(Name = "Connection Name")]
        public string Name { get; set; } = string.Empty;

        [MaxLength(500)]
        [Display(Name = "Base URL")]
        [Url]
        public string BaseUrl { get; set; } = string.Empty;

        [MaxLength(1000)]
        [Display(Name = "Access Token")]
        public string AccessToken { get; set; } = string.Empty;

        [Display(Name = "Active")]
        public bool IsActive { get; set; } = true;

        public DateTime? LastSyncedAt { get; set; }

        // Dropdown lists
        public SelectList? Platforms { get; set; }
    }

    public class ConnectorListViewModel
    {
        public List<ConnectorItemViewModel> Connectors { get; set; } = new List<ConnectorItemViewModel>();
    }

    public class ConnectorItemViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Platform Platform { get; set; }
        public bool IsActive { get; set; }
        public DateTime? LastSyncedAt { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
