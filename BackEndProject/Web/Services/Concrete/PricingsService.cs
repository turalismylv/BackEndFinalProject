using DataAccess.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Web.ViewModels.Price;
using Web.Services.Abstract;

namespace Web.Services.Concrete
{
    public class PricingsService :IPriciningsService
    {

        private readonly ModelStateDictionary _modelState;
        private readonly IPriceRepository _priceRepository;

        public PricingsService(IActionContextAccessor actionContextAccessor, IPriceRepository priceRepository)
        {
            _modelState = actionContextAccessor.ActionContext.ModelState;
            _priceRepository = priceRepository;
        }


        public async Task<PriceIndexVM> GetAllAsync()
        {
            var model = new PriceIndexVM
            {
                Prices = await _priceRepository.GetAllAsync(),

            };
            return model;

        }
    }
}
