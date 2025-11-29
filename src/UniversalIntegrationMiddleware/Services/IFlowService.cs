using System.Security.Claims;
using UniversalIntegrationMiddleware.Models;
using UniversalIntegrationMiddleware.ViewModels;

namespace UniversalIntegrationMiddleware.Services
{
    public interface IFlowService
    {
        Task<FlowListViewModel> GetFlowsForMerchantAsync(ClaimsPrincipal user);
        Task<FlowDetailsViewModel?> GetFlowDetailsAsync(int id, ClaimsPrincipal user);
        Task<IntegrationFlow?> GetFlowByIdAsync(int id, ClaimsPrincipal user);
        Task<int> CreateFlowAsync(ClaimsPrincipal user, CreateFlowViewModel model);
        Task UpdateFlowAsync(int id, ClaimsPrincipal user, EditFlowViewModel model);
        Task DeleteFlowAsync(int id, ClaimsPrincipal user);
        Task ToggleFlowAsync(int id, ClaimsPrincipal user);
    }
}
