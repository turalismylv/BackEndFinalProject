using Microsoft.AspNetCore.Mvc;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.FaqCategory;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class FaqCategoryController : Controller
    {

        private readonly IFaqCategoryService _faqCategoryService;

        public FaqCategoryController(IFaqCategoryService faqCategoryService)
        {

            _faqCategoryService = faqCategoryService;
        }

        #region FaqCategory


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = await _faqCategoryService.GetAllAsync();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Create(FaqCategoryCreateVM model)
        {
            var isSucceeded = await _faqCategoryService.CreateAsync(model);
            if (isSucceeded) return RedirectToAction(nameof(Index));
            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var model = await _faqCategoryService.GetUpdateModelAsync(id);
            if (model == null) return NotFound();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, FaqCategoryUpdateVM model)
        {
            if (id != model.Id) return NotFound();

            var isSucceeded = await _faqCategoryService.UpdateAsync(model);
            if (isSucceeded) return RedirectToAction(nameof(Index));

            model = await _faqCategoryService.GetUpdateModelAsync(id);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var isSucceded = await _faqCategoryService.DeleteAsync(id);
            if (isSucceded)
            {
                return RedirectToAction(nameof(Index));
            }
            return NotFound();

            #endregion
        }
    }
}
