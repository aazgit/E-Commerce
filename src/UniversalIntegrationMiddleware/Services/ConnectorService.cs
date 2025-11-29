using System.Security.Claims;
using UniversalIntegrationMiddleware.Models;
using UniversalIntegrationMiddleware.Models.Enums;
using UniversalIntegrationMiddleware.ViewModels;

namespace UniversalIntegrationMiddleware.Services
{
    public class ConnectorService : IConnectorService
    {
        private static readonly List<ChannelConnection> _connectors = new()
        {
            new ChannelConnection
            {
                Id = 1,
                MerchantId = 1,
                Platform = Platform.Shopify,
                Name = "Shopify – US Store",
                BaseUrl = "https://mystore.myshopify.com",
                AccessToken = "***encrypted***",
                IsActive = true,
                CreatedAt = DateTime.UtcNow.AddDays(-60),
                LastSyncedAt = DateTime.UtcNow.AddMinutes(-15)
            },
            new ChannelConnection
            {
                Id = 2,
                MerchantId = 1,
                Platform = Platform.D365,
                Name = "D365 Finance & Operations",
                BaseUrl = "https://mycompany.dynamics.com",
                AccessToken = "***encrypted***",
                IsActive = true,
                CreatedAt = DateTime.UtcNow.AddDays(-60),
                LastSyncedAt = DateTime.UtcNow.AddMinutes(-15)
            },
            new ChannelConnection
            {
                Id = 3,
                MerchantId = 1,
                Platform = Platform.Amazon,
                Name = "Amazon US Marketplace",
                BaseUrl = "https://sellingpartnerapi.amazon.com",
                AccessToken = "***encrypted***",
                IsActive = true,
                CreatedAt = DateTime.UtcNow.AddDays(-30),
                LastSyncedAt = DateTime.UtcNow.AddHours(-1)
            },
            new ChannelConnection
            {
                Id = 4,
                MerchantId = 1,
                Platform = Platform.VTEX,
                Name = "VTEX Brazil Store",
                BaseUrl = "https://mystore.vtex.com.br",
                AccessToken = "***encrypted***",
                IsActive = false,
                CreatedAt = DateTime.UtcNow.AddDays(-10),
                LastSyncedAt = null
            }
        };

        public Task<ConnectorListViewModel> GetConnectorsForMerchantAsync(ClaimsPrincipal user)
        {
            var viewModel = new ConnectorListViewModel
            {
                Connectors = _connectors.Select(c => new ConnectorItemViewModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    Platform = c.Platform,
                    IsActive = c.IsActive,
                    LastSyncedAt = c.LastSyncedAt,
                    CreatedAt = c.CreatedAt
                }).ToList()
            };
            return Task.FromResult(viewModel);
        }

        public Task<ConnectorViewModel?> GetConnectorByIdAsync(int id, ClaimsPrincipal user)
        {
            var connector = _connectors.FirstOrDefault(c => c.Id == id);
            if (connector == null) return Task.FromResult<ConnectorViewModel?>(null);

            var viewModel = new ConnectorViewModel
            {
                Id = connector.Id,
                Platform = connector.Platform,
                Name = connector.Name,
                BaseUrl = connector.BaseUrl,
                AccessToken = "", // Don't return the actual token
                IsActive = connector.IsActive,
                LastSyncedAt = connector.LastSyncedAt
            };
            return Task.FromResult<ConnectorViewModel?>(viewModel);
        }

        public Task<int> CreateConnectorAsync(ClaimsPrincipal user, ConnectorViewModel model)
        {
            var newId = _connectors.Count > 0 ? _connectors.Max(c => c.Id) + 1 : 1;
            var connector = new ChannelConnection
            {
                Id = newId,
                MerchantId = 1,
                Platform = model.Platform,
                Name = model.Name,
                BaseUrl = model.BaseUrl,
                AccessToken = model.AccessToken,
                IsActive = model.IsActive,
                CreatedAt = DateTime.UtcNow
            };
            _connectors.Add(connector);
            return Task.FromResult(newId);
        }

        public Task UpdateConnectorAsync(int id, ClaimsPrincipal user, ConnectorViewModel model)
        {
            var connector = _connectors.FirstOrDefault(c => c.Id == id);
            if (connector != null)
            {
                connector.Platform = model.Platform;
                connector.Name = model.Name;
                connector.BaseUrl = model.BaseUrl;
                if (!string.IsNullOrEmpty(model.AccessToken))
                {
                    connector.AccessToken = model.AccessToken;
                }
                connector.IsActive = model.IsActive;
            }
            return Task.CompletedTask;
        }

        public Task DeleteConnectorAsync(int id, ClaimsPrincipal user)
        {
            var connector = _connectors.FirstOrDefault(c => c.Id == id);
            if (connector != null)
            {
                _connectors.Remove(connector);
            }
            return Task.CompletedTask;
        }

        public Task<bool> TestConnectionAsync(int id, ClaimsPrincipal user)
        {
            // In a real implementation, this would actually test the connection
            var connector = _connectors.FirstOrDefault(c => c.Id == id);
            return Task.FromResult(connector?.IsActive ?? false);
        }

        public Task ToggleStatusAsync(int id, ClaimsPrincipal user)
        {
            var connector = _connectors.FirstOrDefault(c => c.Id == id);
            if (connector != null)
            {
                connector.IsActive = !connector.IsActive;
            }
            return Task.CompletedTask;
        }
    }
}
