using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.LastestNew;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class LastestNewController : Controller
    {
        private readonly ILastestNewService _lastestNewService;

        public LastestNewController(ILastestNewService lastestNewService)
        {
            _lastestNewService = lastestNewService;
        }

        #region LastestNew


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = await _lastestNewService.GetAllAsync();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Create(LastestNewCreateVM model)
        {
            var isSucceeded = await _lastestNewService.CreateAsync(model);
            if (isSucceeded) return RedirectToAction(nameof(Index));
            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var model = await _lastestNewService.GetUpdateModelAsync(id);
            if (model == null) return NotFound();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, LastestNewUpdateVM model)
        {
            if (id != model.Id) return NotFound();

            var isSucceeded = await _lastestNewService.UpdateAsync(model);
            if (isSucceeded) return RedirectToAction(nameof(Index));

            model = await _lastestNewService.GetUpdateModelAsync(id);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var isSucceded = await _lastestNewService.DeleteAsync(id);
            if (isSucceded)
            {
                return RedirectToAction(nameof(Index));
            }
            return NotFound();


        }

        #endregion
    }
}
