using System.Security.Claims;
using UniversalIntegrationMiddleware.ViewModels;
using LogLevel = UniversalIntegrationMiddleware.Models.Enums.LogLevel;

namespace UniversalIntegrationMiddleware.Services
{
    public interface ILogService
    {
        Task<LogListViewModel> GetLogsAsync(ClaimsPrincipal user, LogLevel? level = null, int? flowId = null, int? jobId = null, DateTime? startDate = null, DateTime? endDate = null);
        Task<LogDetailsViewModel?> GetLogDetailsAsync(int id, ClaimsPrincipal user);
    }
}
