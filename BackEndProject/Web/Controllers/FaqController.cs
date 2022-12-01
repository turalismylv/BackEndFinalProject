using Microsoft.AspNetCore.Mvc;
using Web.Services.Abstract;

namespace Web.Controllers
{
    public class FaqController : Controller
    {

        private readonly IFaqService _faqService;

        public FaqController(IFaqService faqService)
        {
         
            _faqService = faqService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = await _faqService.GetAllAsync();
            return View(model);
        }

        public async Task<IActionResult> CategoryQuestion(int id)
        {
            var model = await _faqService.CategoryQuestionAsync(id);

            return PartialView("_QuestionPartial", model);

        }
    }
}
