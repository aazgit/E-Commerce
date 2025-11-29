using Microsoft.AspNetCore.Mvc;
using UniversalIntegrationMiddleware.Services;
using UniversalIntegrationMiddleware.ViewModels;

namespace UniversalIntegrationMiddleware.Controllers
{
    public class MappingController : Controller
    {
        private readonly IMappingService _mappingService;

        public MappingController(IMappingService mappingService)
        {
            _mappingService = mappingService;
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = await _mappingService.GetMappingsForMerchantAsync(User);
            return View(viewModel);
        }

        public IActionResult Create()
        {
            return View(new MappingViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MappingViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await _mappingService.CreateMappingAsync(User, model);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var mapping = await _mappingService.GetMappingByIdAsync(id, User);
            if (mapping == null) return NotFound();
            return View(mapping);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, MappingViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await _mappingService.UpdateMappingAsync(id, User, model);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Preview(int id, string inputJson)
        {
            var result = await _mappingService.PreviewTransformationAsync(id, inputJson, User);
            return Json(new { output = result });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _mappingService.DeleteMappingAsync(id, User);
            return RedirectToAction(nameof(Index));
        }
    }
}
