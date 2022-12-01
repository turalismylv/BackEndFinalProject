using Microsoft.AspNetCore.Mvc;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.HomeChooseComponent;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeChooseComponentController : Controller
    {
      
        private readonly IHomeChooseComponentService _homeChooseComponentService;

        public HomeChooseComponentController(IHomeChooseComponentService homeChooseComponentService)
        {

           
            _homeChooseComponentService = homeChooseComponentService;
        }
        [HttpGet]

        public async Task<IActionResult> Index()
        {
            var model = await _homeChooseComponentService.GetAsync();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var homeChooseComponent = await _homeChooseComponentService.GetAsync();

            if (homeChooseComponent.HomeChooseComponent != null)
            {
                return NotFound();
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(HomeChooseComponentCreateVM model)
        {


            var isSucceeded = await _homeChooseComponentService.CreateAsync(model);
            if (isSucceeded) return RedirectToAction(nameof(Index));
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var model = await _homeChooseComponentService.GetUpdateModelAsync(id);
            if (model == null) return NotFound();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, HomeChooseComponentUpdateVM model)
        {
            if (id != model.Id) return NotFound();

            var isSucceeded = await _homeChooseComponentService.UpdateAsync(model);
            if (isSucceeded) return RedirectToAction(nameof(Index));

            model = await _homeChooseComponentService.GetUpdateModelAsync(id);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var isSucceded = await _homeChooseComponentService.DeleteAsync(id);
            if (isSucceded)
            {
                return RedirectToAction(nameof(Index));
            }
            return NotFound();

        }

    }
}
