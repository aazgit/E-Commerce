using System.Security.Claims;
using UniversalIntegrationMiddleware.Models;
using UniversalIntegrationMiddleware.Models.Enums;
using UniversalIntegrationMiddleware.ViewModels;

namespace UniversalIntegrationMiddleware.Services
{
    public class FlowService : IFlowService
    {
        private static readonly List<IntegrationFlow> _flows = new()
        {
            new IntegrationFlow
            {
                Id = 1,
                MerchantId = 1,
                Name = "Shopify → D365 Order Sync",
                SourceConnectionId = 1,
                TargetConnectionId = 2,
                FlowType = FlowType.Orders,
                IsEnabled = true,
                CreatedAt = DateTime.UtcNow.AddDays(-30)
            },
            new IntegrationFlow
            {
                Id = 2,
                MerchantId = 1,
                Name = "D365 → Shopify Inventory Sync",
                SourceConnectionId = 2,
                TargetConnectionId = 1,
                FlowType = FlowType.Inventory,
                IsEnabled = true,
                CreatedAt = DateTime.UtcNow.AddDays(-20)
            },
            new IntegrationFlow
            {
                Id = 3,
                MerchantId = 1,
                Name = "Amazon → D365 Products",
                SourceConnectionId = 3,
                TargetConnectionId = 2,
                FlowType = FlowType.Products,
                IsEnabled = false,
                CreatedAt = DateTime.UtcNow.AddDays(-10)
            }
        };

        public Task<FlowListViewModel> GetFlowsForMerchantAsync(ClaimsPrincipal user)
        {
            var viewModel = new FlowListViewModel
            {
                Flows = _flows.Select(f => new FlowListItemViewModel
                {
                    Id = f.Id,
                    Name = f.Name,
                    FlowType = f.FlowType,
                    SourceConnectionName = GetConnectionName(f.SourceConnectionId),
                    TargetConnectionName = GetConnectionName(f.TargetConnectionId),
                    ScheduleDescription = f.ScheduleId.HasValue ? "Every 15 minutes" : "Manual",
                    IsEnabled = f.IsEnabled,
                    LastJobStatus = JobStatus.Success
                }).ToList()
            };
            return Task.FromResult(viewModel);
        }

        public Task<FlowDetailsViewModel?> GetFlowDetailsAsync(int id, ClaimsPrincipal user)
        {
            var flow = _flows.FirstOrDefault(f => f.Id == id);
            if (flow == null) return Task.FromResult<FlowDetailsViewModel?>(null);

            var viewModel = new FlowDetailsViewModel
            {
                Id = flow.Id,
                Name = flow.Name,
                SourceConnectionName = GetConnectionName(flow.SourceConnectionId),
                TargetConnectionName = GetConnectionName(flow.TargetConnectionId),
                FlowType = flow.FlowType,
                MappingTemplateName = flow.MappingTemplateId.HasValue ? "Default Mapping" : null,
                ScheduleDescription = flow.ScheduleId.HasValue ? "Every 15 minutes" : "Manual",
                IsEnabled = flow.IsEnabled,
                CreatedAt = flow.CreatedAt,
                RecentJobs = new List<JobRun>()
            };
            return Task.FromResult<FlowDetailsViewModel?>(viewModel);
        }

        public Task<IntegrationFlow?> GetFlowByIdAsync(int id, ClaimsPrincipal user)
        {
            return Task.FromResult(_flows.FirstOrDefault(f => f.Id == id));
        }

        public Task<int> CreateFlowAsync(ClaimsPrincipal user, CreateFlowViewModel model)
        {
            var newId = _flows.Count > 0 ? _flows.Max(f => f.Id) + 1 : 1;
            var flow = new IntegrationFlow
            {
                Id = newId,
                MerchantId = 1,
                Name = model.Name,
                SourceConnectionId = model.SourceConnectionId,
                TargetConnectionId = model.TargetConnectionId,
                FlowType = model.FlowType,
                MappingTemplateId = model.MappingTemplateId,
                ScheduleId = model.ScheduleId,
                IsEnabled = model.IsEnabled,
                CreatedAt = DateTime.UtcNow
            };
            _flows.Add(flow);
            return Task.FromResult(newId);
        }

        public Task UpdateFlowAsync(int id, ClaimsPrincipal user, EditFlowViewModel model)
        {
            var flow = _flows.FirstOrDefault(f => f.Id == id);
            if (flow != null)
            {
                flow.Name = model.Name;
                flow.SourceConnectionId = model.SourceConnectionId;
                flow.TargetConnectionId = model.TargetConnectionId;
                flow.FlowType = model.FlowType;
                flow.MappingTemplateId = model.MappingTemplateId;
                flow.ScheduleId = model.ScheduleId;
                flow.IsEnabled = model.IsEnabled;
            }
            return Task.CompletedTask;
        }

        public Task DeleteFlowAsync(int id, ClaimsPrincipal user)
        {
            var flow = _flows.FirstOrDefault(f => f.Id == id);
            if (flow != null)
            {
                _flows.Remove(flow);
            }
            return Task.CompletedTask;
        }

        public Task ToggleFlowAsync(int id, ClaimsPrincipal user)
        {
            var flow = _flows.FirstOrDefault(f => f.Id == id);
            if (flow != null)
            {
                flow.IsEnabled = !flow.IsEnabled;
            }
            return Task.CompletedTask;
        }

        private static string GetConnectionName(int connectionId)
        {
            return connectionId switch
            {
                1 => "Shopify – US Store",
                2 => "D365 Finance & Operations",
                3 => "Amazon US Marketplace",
                _ => "Unknown"
            };
        }
    }
}
