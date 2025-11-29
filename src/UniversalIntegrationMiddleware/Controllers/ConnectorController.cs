using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using UniversalIntegrationMiddleware.Models.Enums;
using UniversalIntegrationMiddleware.Services;
using UniversalIntegrationMiddleware.ViewModels;

namespace UniversalIntegrationMiddleware.Controllers
{
    public class ConnectorController : Controller
    {
        private readonly IConnectorService _connectorService;

        public ConnectorController(IConnectorService connectorService)
        {
            _connectorService = connectorService;
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = await _connectorService.GetConnectorsForMerchantAsync(User);
            return View(viewModel);
        }

        public IActionResult Create()
        {
            var viewModel = new ConnectorViewModel
            {
                Platforms = new SelectList(Enum.GetValues<Platform>())
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ConnectorViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Platforms = new SelectList(Enum.GetValues<Platform>());
                return View(model);
            }

            await _connectorService.CreateConnectorAsync(User, model);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var connector = await _connectorService.GetConnectorByIdAsync(id, User);
            if (connector == null) return NotFound();

            connector.Platforms = new SelectList(Enum.GetValues<Platform>());
            return View(connector);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ConnectorViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Platforms = new SelectList(Enum.GetValues<Platform>());
                return View(model);
            }

            await _connectorService.UpdateConnectorAsync(id, User, model);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TestConnection(int id)
        {
            var success = await _connectorService.TestConnectionAsync(id, User);
            TempData["TestResult"] = success ? "Connection successful!" : "Connection failed.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleStatus(int id)
        {
            await _connectorService.ToggleStatusAsync(id, User);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _connectorService.DeleteConnectorAsync(id, User);
            return RedirectToAction(nameof(Index));
        }
    }
}
