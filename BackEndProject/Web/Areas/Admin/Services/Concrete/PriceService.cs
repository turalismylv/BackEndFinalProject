using Core.Entities;
using DataAccess.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.Price;

namespace Web.Areas.Admin.Services.Concrete
{
    public class PriceService :IPriceService
    {

       
    
        private readonly ModelStateDictionary _modelState;
        private readonly IPriceRepository _priceRepository;

        public PriceService(IPriceRepository priceRepository, IActionContextAccessor actionContextAccessor)
        {
            _modelState = actionContextAccessor.ActionContext.ModelState;
            _priceRepository = priceRepository;
        }

        public async Task<PriceIndexVM> GetAllAsync()
        {
            var model = new PriceIndexVM
            {
                Prices = await _priceRepository.GetAllAsync()
            };
            return model;
        }

        public async Task<bool> CreateAsync(PriceCreateVM model)
        {
            if (!_modelState.IsValid) return false;

            var isExist = await _priceRepository.AnyAsync(c => c.Title.Trim().ToLower() == model.Title.Trim().ToLower());
            if (isExist)
            {
                _modelState.AddModelError("Title", "Bu adda ourvision mövcuddur");
                return false;
            }
            
            var price = new Price
            {
                
                Title = model.Title,
                Description = model.Description,
                CreatedAt = DateTime.Now,
                Cost=model.Cost,
                Feature=model.Feature,
                
            };

            await _priceRepository.CreateAsync(price);
            return true;
        }

        public async Task<PriceUpdateVM> GetUpdateModelAsync(int id)
        {


            var price = await _priceRepository.GetAsync(id);

            if (price == null) return null;

            var model = new PriceUpdateVM
            {
                Id = price.Id,
                Title = price.Title,
                Description = price.Description,
               Cost=price.Cost,
               Feature=price.Feature

            };

            return model;

        }

        public async Task<bool> UpdateAsync(PriceUpdateVM model)
        {
            if (!_modelState.IsValid) return false;

            var isExist = await _priceRepository.AnyAsync(c => c.Title.Trim().ToLower() == model.Title.Trim().ToLower() && c.Id != model.Id);
            if (isExist)
            {
                _modelState.AddModelError("Title", "Bu adda ourvision mövcuddur");
                return false;
            }

            var price = await _priceRepository.GetAsync(model.Id);

            if (price != null)
            {
                price.Id = model.Id;
                price.Title = model.Title;
                price.ModifiedAt = DateTime.Now;
                price.Description = model.Description;
                price.Feature = model.Feature;
                price.Cost = model.Cost;

                await _priceRepository.UpdateAsync(price);

            }
            return true;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var price = await _priceRepository.GetAsync(id);
            if (price != null)
            {
                await _priceRepository.DeleteAsync(price);

                return true;

            }

            return false;
        }
    }
}
