using Microsoft.AspNetCore.Mvc;
using Web.Services.Abstract;

namespace Web.Controllers
{
    public class ShopController : Controller
    {
        private readonly IShopService _shopService;

        public ShopController(IShopService shopService)
        {

            _shopService = shopService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = await _shopService.GetAllAsync();
            return View(model);
        }

        public async Task<IActionResult> CategoryProduct(int id)
        {
            var model = await _shopService.CategoryProductAsync(id);

            return PartialView("_ProductPartial", model);

        }
    }
}
