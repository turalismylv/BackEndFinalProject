using Microsoft.AspNetCore.Mvc;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.HomeVideoComponent;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeVideoComponentController : Controller
    {

        private readonly IHomeVideoComponentService _homeVideoComponentService;

        public HomeVideoComponentController(IHomeVideoComponentService homeVideoComponentService)
        {

            _homeVideoComponentService = homeVideoComponentService;
        }



      


        [HttpGet]

        public async Task<IActionResult> Index()
        {
            var model = await _homeVideoComponentService.GetAsync();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var homeVideoComponent = await _homeVideoComponentService.GetAsync();

            if (homeVideoComponent.HomeVideoComponent != null)
            {
                return NotFound();
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(HomeVideoComponentCreateVM model)
        {


            var isSucceeded = await _homeVideoComponentService.CreateAsync(model);
            if (isSucceeded) return RedirectToAction(nameof(Index));
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var model = await _homeVideoComponentService.GetUpdateModelAsync(id);
            if (model == null) return NotFound();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, HomeVideoComponentUpdateVM model)
        {
            if (id != model.Id) return NotFound();

            var isSucceeded = await _homeVideoComponentService.UpdateAsync(model);
            if (isSucceeded) return RedirectToAction(nameof(Index));

            model = await _homeVideoComponentService.GetUpdateModelAsync(id);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var isSucceded = await _homeVideoComponentService.DeleteAsync(id);
            if (isSucceded)
            {
                return RedirectToAction(nameof(Index));
            }
            return NotFound();

        }

    }
}
