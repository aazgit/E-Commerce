using System.Security.Claims;
using UniversalIntegrationMiddleware.Models;
using UniversalIntegrationMiddleware.ViewModels;

namespace UniversalIntegrationMiddleware.Services
{
    public interface IMappingService
    {
        Task<MappingListViewModel> GetMappingsForMerchantAsync(ClaimsPrincipal user);
        Task<MappingViewModel?> GetMappingByIdAsync(int id, ClaimsPrincipal user);
        Task<int> CreateMappingAsync(ClaimsPrincipal user, MappingViewModel model);
        Task UpdateMappingAsync(int id, ClaimsPrincipal user, MappingViewModel model);
        Task DeleteMappingAsync(int id, ClaimsPrincipal user);
        Task<string> PreviewTransformationAsync(int id, string inputJson, ClaimsPrincipal user);
    }
}
