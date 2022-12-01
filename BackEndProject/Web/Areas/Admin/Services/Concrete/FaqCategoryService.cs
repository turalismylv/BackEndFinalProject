using Core.Entities;
using DataAccess.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.FaqCategory;

namespace Web.Areas.Admin.Services.Concrete
{
    public class FaqCategoryService :IFaqCategoryService
    {
        private readonly ModelStateDictionary _modelState;
        private readonly IFaqCategoryRepository _faqCategoryRepository;

        public FaqCategoryService(IFaqCategoryRepository faqCategoryRepository, IActionContextAccessor actionContextAccessor)
        {
            _modelState = actionContextAccessor.ActionContext.ModelState;
           
            _faqCategoryRepository = faqCategoryRepository;
        }

        public async Task<FaqCategoryIndexVM> GetAllAsync()
        {
            var model = new FaqCategoryIndexVM
            {
                FaqCategories = await _faqCategoryRepository.GetAllAsync()
            };
            return model;
        }

        public async Task<bool> CreateAsync(FaqCategoryCreateVM model)
        {
            if (!_modelState.IsValid) return false;

            var isExist = await _faqCategoryRepository.AnyAsync(c => c.Title.Trim().ToLower() == model.Title.Trim().ToLower());
            if (isExist)
            {
                _modelState.AddModelError("Title", "Bu adda ourvision mövcuddur");
                return false;
            }

            var faqCategory = new FaqCategory
            {

                Title = model.Title,
                Description = model.Description,
                CreatedAt = DateTime.Now,
               

            };

            await _faqCategoryRepository.CreateAsync(faqCategory);
            return true;
        }

        public async Task<FaqCategoryUpdateVM> GetUpdateModelAsync(int id)
        {


            var faqCategory = await _faqCategoryRepository.GetAsync(id);

            if (faqCategory == null) return null;

            var model = new FaqCategoryUpdateVM
            {
                Id = faqCategory.Id,
                Title = faqCategory.Title,
                Description = faqCategory.Description,
               

            };

            return model;

        }

        public async Task<bool> UpdateAsync(FaqCategoryUpdateVM model)
        {
            if (!_modelState.IsValid) return false;

            var isExist = await _faqCategoryRepository.AnyAsync(c => c.Title.Trim().ToLower() == model.Title.Trim().ToLower() && c.Id != model.Id);
            if (isExist)
            {
                _modelState.AddModelError("Title", "Bu adda ourvision mövcuddur");
                return false;
            }

            var faqCategory = await _faqCategoryRepository.GetAsync(model.Id);

            if (faqCategory != null)
            {
                faqCategory.Id = model.Id;
                faqCategory.Title = model.Title;
                faqCategory.ModifiedAt = DateTime.Now;
                faqCategory.Description = model.Description;
              

                await _faqCategoryRepository.UpdateAsync(faqCategory);

            }
            return true;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var price = await _faqCategoryRepository.GetAsync(id);
            if (price != null)
            {
                await _faqCategoryRepository.DeleteAsync(price);

                return true;

            }

            return false;
        }

    }
}
