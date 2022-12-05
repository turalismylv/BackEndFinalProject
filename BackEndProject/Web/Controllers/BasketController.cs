using Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Services.Abstract;
using Web.ViewModels.Basket;

namespace Web.Controllers
{
    [Authorize]
    public class BasketController : Controller
    {
        private readonly IBasketService _basketService;

        public BasketController(IBasketService basketService)
        {
            _basketService = basketService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()

        {

           var model = await _basketService.GetAsync(User);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(BasketAddVM model)
        {
            var isSucceeded = await _basketService.Add(model, User);
            if (isSucceeded) return Ok();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var isSucceeded = await _basketService.DeleteBasketProduct(id,User);
            if (isSucceeded) return Ok();
            return View();

        }

        [HttpPost]
        public async Task<IActionResult> UpCountProduct(int id)
        {
            var isSucceeded = await _basketService.UpCount(id, User);
            if (isSucceeded) return RedirectToAction(nameof(Index));
            return View();

        }

        [HttpPost]
        public async Task<IActionResult> DownCountProduct(int id)
        {
            var isSucceeded = await _basketService.DownCount(id, User);
            if (isSucceeded) return RedirectToAction(nameof(Index));
            return View();

        }

        [HttpPost]
        public async Task<IActionResult> Clear()
        {
            var isSucceeded = await _basketService.ClearBasketProduct(User);
            if (isSucceeded) return RedirectToAction(nameof(Index));
            return View();

        }
    }
}
