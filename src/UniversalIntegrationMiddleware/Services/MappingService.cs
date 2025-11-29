using System.Security.Claims;
using UniversalIntegrationMiddleware.Models;
using UniversalIntegrationMiddleware.ViewModels;

namespace UniversalIntegrationMiddleware.Services
{
    public class MappingService : IMappingService
    {
        private static readonly List<MappingTemplate> _mappings = new()
        {
            new MappingTemplate
            {
                Id = 1,
                MerchantId = 1,
                Name = "Shopify Order to D365 Sales Order",
                SourceSchema = @"{
                    ""type"": ""object"",
                    ""properties"": {
                        ""id"": { ""type"": ""integer"" },
                        ""name"": { ""type"": ""string"" },
                        ""total_price"": { ""type"": ""string"" },
                        ""line_items"": { ""type"": ""array"" }
                    }
                }",
                TargetSchema = @"{
                    ""type"": ""object"",
                    ""properties"": {
                        ""SalesOrderNumber"": { ""type"": ""string"" },
                        ""CustomerName"": { ""type"": ""string"" },
                        ""TotalAmount"": { ""type"": ""number"" },
                        ""Lines"": { ""type"": ""array"" }
                    }
                }",
                TransformationRules = @"{
                    ""SalesOrderNumber"": ""$.name"",
                    ""TotalAmount"": ""$.total_price"",
                    ""Lines"": ""$.line_items""
                }",
                CreatedAt = DateTime.UtcNow.AddDays(-45),
                UpdatedAt = DateTime.UtcNow.AddDays(-10)
            },
            new MappingTemplate
            {
                Id = 2,
                MerchantId = 1,
                Name = "D365 Inventory to Shopify",
                SourceSchema = @"{
                    ""type"": ""object"",
                    ""properties"": {
                        ""ItemId"": { ""type"": ""string"" },
                        ""AvailableQuantity"": { ""type"": ""number"" },
                        ""WarehouseId"": { ""type"": ""string"" }
                    }
                }",
                TargetSchema = @"{
                    ""type"": ""object"",
                    ""properties"": {
                        ""sku"": { ""type"": ""string"" },
                        ""available"": { ""type"": ""integer"" },
                        ""location_id"": { ""type"": ""string"" }
                    }
                }",
                TransformationRules = @"{
                    ""sku"": ""$.ItemId"",
                    ""available"": ""$.AvailableQuantity"",
                    ""location_id"": ""$.WarehouseId""
                }",
                CreatedAt = DateTime.UtcNow.AddDays(-30),
                UpdatedAt = DateTime.UtcNow.AddDays(-5)
            }
        };

        public Task<MappingListViewModel> GetMappingsForMerchantAsync(ClaimsPrincipal user)
        {
            var viewModel = new MappingListViewModel
            {
                Mappings = _mappings.Select(m => new MappingListItemViewModel
                {
                    Id = m.Id,
                    Name = m.Name,
                    CreatedAt = m.CreatedAt,
                    UpdatedAt = m.UpdatedAt,
                    FlowsUsingCount = GetFlowsUsingCount(m.Id)
                }).ToList()
            };
            return Task.FromResult(viewModel);
        }

        public Task<MappingViewModel?> GetMappingByIdAsync(int id, ClaimsPrincipal user)
        {
            var mapping = _mappings.FirstOrDefault(m => m.Id == id);
            if (mapping == null) return Task.FromResult<MappingViewModel?>(null);

            var viewModel = new MappingViewModel
            {
                Id = mapping.Id,
                Name = mapping.Name,
                SourceSchema = mapping.SourceSchema,
                TargetSchema = mapping.TargetSchema,
                TransformationRules = mapping.TransformationRules,
                CreatedAt = mapping.CreatedAt,
                UpdatedAt = mapping.UpdatedAt
            };
            return Task.FromResult<MappingViewModel?>(viewModel);
        }

        public Task<int> CreateMappingAsync(ClaimsPrincipal user, MappingViewModel model)
        {
            var newId = _mappings.Count > 0 ? _mappings.Max(m => m.Id) + 1 : 1;
            var mapping = new MappingTemplate
            {
                Id = newId,
                MerchantId = 1,
                Name = model.Name,
                SourceSchema = model.SourceSchema,
                TargetSchema = model.TargetSchema,
                TransformationRules = model.TransformationRules,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            _mappings.Add(mapping);
            return Task.FromResult(newId);
        }

        public Task UpdateMappingAsync(int id, ClaimsPrincipal user, MappingViewModel model)
        {
            var mapping = _mappings.FirstOrDefault(m => m.Id == id);
            if (mapping != null)
            {
                mapping.Name = model.Name;
                mapping.SourceSchema = model.SourceSchema;
                mapping.TargetSchema = model.TargetSchema;
                mapping.TransformationRules = model.TransformationRules;
                mapping.UpdatedAt = DateTime.UtcNow;
            }
            return Task.CompletedTask;
        }

        public Task DeleteMappingAsync(int id, ClaimsPrincipal user)
        {
            var mapping = _mappings.FirstOrDefault(m => m.Id == id);
            if (mapping != null)
            {
                _mappings.Remove(mapping);
            }
            return Task.CompletedTask;
        }

        public Task<string> PreviewTransformationAsync(int id, string inputJson, ClaimsPrincipal user)
        {
            // In a real implementation, this would apply the transformation rules
            return Task.FromResult(@"{
                ""SalesOrderNumber"": ""#1001"",
                ""TotalAmount"": 199.99,
                ""Lines"": []
            }");
        }

        private static int GetFlowsUsingCount(int mappingId)
        {
            return mappingId == 1 ? 2 : 1;
        }
    }
}
