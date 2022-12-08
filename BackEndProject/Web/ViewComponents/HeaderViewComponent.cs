using DataAccess.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc;
using Web.ViewModels.Header;

namespace Web.ViewComponents
{
    public class HeaderViewComponent :ViewComponent
    {
        private readonly IBasketProductRepository _basketProductRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HeaderViewComponent(IBasketProductRepository basketProductRepository, IHttpContextAccessor httpContextAccessor)
        {
            _basketProductRepository = basketProductRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {

            var model = new HeaderViewComponentVM
            {
                Count = await _basketProductRepository.GetUserBasketProductCount(_httpContextAccessor.HttpContext.User)
            };

            return View(model);
        }
    }
}
