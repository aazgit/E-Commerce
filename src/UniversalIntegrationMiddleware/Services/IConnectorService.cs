using System.Security.Claims;
using UniversalIntegrationMiddleware.Models;
using UniversalIntegrationMiddleware.ViewModels;

namespace UniversalIntegrationMiddleware.Services
{
    public interface IConnectorService
    {
        Task<ConnectorListViewModel> GetConnectorsForMerchantAsync(ClaimsPrincipal user);
        Task<ConnectorViewModel?> GetConnectorByIdAsync(int id, ClaimsPrincipal user);
        Task<int> CreateConnectorAsync(ClaimsPrincipal user, ConnectorViewModel model);
        Task UpdateConnectorAsync(int id, ClaimsPrincipal user, ConnectorViewModel model);
        Task DeleteConnectorAsync(int id, ClaimsPrincipal user);
        Task<bool> TestConnectionAsync(int id, ClaimsPrincipal user);
        Task ToggleStatusAsync(int id, ClaimsPrincipal user);
    }
}
