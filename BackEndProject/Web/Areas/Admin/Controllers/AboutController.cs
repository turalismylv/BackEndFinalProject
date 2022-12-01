using Microsoft.AspNetCore.Mvc;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.AboutHome;
using Web.Areas.Admin.ViewModels.AboutHome.AboutHomePhoto;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AboutController : Controller
    {
        
        private readonly IAboutService _aboutService;

        public AboutController(IAboutService aboutService)
        {
         
            _aboutService = aboutService;
        }



        #region AboutHome


        [HttpGet]

        public async Task<IActionResult> Index()
        {
            var model = await _aboutService.GetAsync();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var about = await _aboutService.GetAsync();

            if (about.AboutHome != null)
            {
                return NotFound();
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AboutCreateVM model)
        {


            var isSucceeded = await _aboutService.CreateAsync(model);
            if (isSucceeded) return RedirectToAction(nameof(Index));
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var model = await _aboutService.GetUpdateModelAsync(id);
            if (model == null) return NotFound();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, AboutUpdateVM model)
        {
            if (id != model.Id) return NotFound();

            var isSucceeded = await _aboutService.UpdateAsync(model);
            if (isSucceeded) return RedirectToAction(nameof(Index));

            model = await _aboutService.GetUpdateModelAsync(id);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var isSucceded = await _aboutService.DeleteAsync(id);
            if (isSucceded)
            {
                return RedirectToAction(nameof(Index));
            }
            return NotFound();

        }


        [HttpPost]
        public async Task<IActionResult> DeletePhoto(int id, int aboutHomeId)
        {

            var isSucceded = await _aboutService.DeletePhotoAsync(id);
            if (isSucceded) return RedirectToAction("update", "about", new { id = aboutHomeId });

            return NotFound();


        }

        [HttpGet]
        public async Task<IActionResult> UpdatePhoto(int id)
        {
            var model = await _aboutService.GetPhotoUpdateModelAsync(id);
            if (model == null) return NotFound();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePhoto(int id, AboutPhotoUpdateVM model)
        {
            if (id != model.Id) return NotFound();

            var isSucceeded = await _aboutService.UpdatePhotoAsync(model);
            if (isSucceeded) return RedirectToAction("update", "about", new { id = model.AboutHomeId });

            return View(model);
        }

        #endregion
    }
}
