using Microsoft.AspNetCore.Mvc;
using Web.Services.Abstract;

namespace Web.Controllers
{
    public class PricingsController : Controller
    {
        private readonly IPriciningsService _priciningsService;

        public PricingsController(IPriciningsService priciningsService)
        {
            _priciningsService = priciningsService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = await _priciningsService.GetAllAsync();
            return View(model);
        }
    }
}
