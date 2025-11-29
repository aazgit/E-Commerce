using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using UniversalIntegrationMiddleware.Models.Enums;
using UniversalIntegrationMiddleware.Services;
using UniversalIntegrationMiddleware.ViewModels;

namespace UniversalIntegrationMiddleware.Controllers
{
    public class FlowController : Controller
    {
        private readonly IFlowService _flowService;
        private readonly IJobService _jobService;
        private readonly IConnectorService _connectorService;
        private readonly IMappingService _mappingService;

        public FlowController(
            IFlowService flowService,
            IJobService jobService,
            IConnectorService connectorService,
            IMappingService mappingService)
        {
            _flowService = flowService;
            _jobService = jobService;
            _connectorService = connectorService;
            _mappingService = mappingService;
        }

        public async Task<IActionResult> Index()
        {
            var flows = await _flowService.GetFlowsForMerchantAsync(User);
            return View(flows);
        }

        public async Task<IActionResult> Create()
        {
            var vm = await PopulateCreateFlowViewModelAsync(new CreateFlowViewModel());
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateFlowViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model = await PopulateCreateFlowViewModelAsync(model);
                return View(model);
            }

            await _flowService.CreateFlowAsync(User, model);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var flow = await _flowService.GetFlowByIdAsync(id, User);
            if (flow == null) return NotFound();

            var vm = new EditFlowViewModel
            {
                Id = flow.Id,
                Name = flow.Name,
                SourceConnectionId = flow.SourceConnectionId,
                TargetConnectionId = flow.TargetConnectionId,
                FlowType = flow.FlowType,
                MappingTemplateId = flow.MappingTemplateId,
                ScheduleId = flow.ScheduleId,
                IsEnabled = flow.IsEnabled
            };

            vm = (EditFlowViewModel)await PopulateCreateFlowViewModelAsync(vm);
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditFlowViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model = (EditFlowViewModel)await PopulateCreateFlowViewModelAsync(model);
                return View(model);
            }

            await _flowService.UpdateFlowAsync(id, User, model);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RunNow(int id)
        {
            await _jobService.TriggerJobAsync(id, User);
            return RedirectToAction(nameof(Details), new { id });
        }

        public async Task<IActionResult> Details(int id)
        {
            var flow = await _flowService.GetFlowDetailsAsync(id, User);
            if (flow == null) return NotFound();
            return View(flow);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Toggle(int id)
        {
            await _flowService.ToggleFlowAsync(id, User);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _flowService.DeleteFlowAsync(id, User);
            return RedirectToAction(nameof(Index));
        }

        private async Task<CreateFlowViewModel> PopulateCreateFlowViewModelAsync(CreateFlowViewModel model)
        {
            var connectors = await _connectorService.GetConnectorsForMerchantAsync(User);
            var mappings = await _mappingService.GetMappingsForMerchantAsync(User);

            model.SourceConnections = new SelectList(
                connectors.Connectors.Select(c => new { c.Id, c.Name }),
                "Id", "Name", model.SourceConnectionId);

            model.TargetConnections = new SelectList(
                connectors.Connectors.Select(c => new { c.Id, c.Name }),
                "Id", "Name", model.TargetConnectionId);

            model.FlowTypes = new SelectList(Enum.GetValues<FlowType>());

            model.MappingTemplates = new SelectList(
                mappings.Mappings.Select(m => new { m.Id, m.Name }),
                "Id", "Name", model.MappingTemplateId);

            // Note: Schedules would be populated similarly if we had a schedule service
            model.Schedules = new SelectList(new List<object>());

            return model;
        }
    }
}
