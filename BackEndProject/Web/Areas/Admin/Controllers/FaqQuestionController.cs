using Microsoft.AspNetCore.Mvc;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.FaqQuestion;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class FaqQuestionController : Controller
    {
        private readonly IFaqQuestionService _faqQuestionService;

        public FaqQuestionController(IFaqQuestionService faqQuestionService)
        {

            _faqQuestionService = faqQuestionService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = await _faqQuestionService.GetAllAsync();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = await _faqQuestionService.GetCreateModelAsync();
            return View(model);
        }

        [HttpPost]

        public async Task<IActionResult> Create(FaqQuestionCreateVM model)
        {


            var isSucceeded = await _faqQuestionService.CreateAsync(model);
            if (isSucceeded) return RedirectToAction(nameof(Index));
            model = await _faqQuestionService.GetCreateModelAsync();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var model = await _faqQuestionService.GetUpdateModelAsync(id);
            if (model == null) return NotFound();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, FaqQuestionUpdateVM model)
        {
            if (id != model.Id) return NotFound();

            var isSucceeded = await _faqQuestionService.UpdateAsync(model);
            if (isSucceeded) return RedirectToAction(nameof(Index));

            model = await _faqQuestionService.GetUpdateModelAsync(id);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var isSucceded = await _faqQuestionService.DeleteAsync(id);
            if (isSucceded)
            {
                return RedirectToAction(nameof(Index));
            }
            return NotFound();


        }
    }
}
