using Core.Entities;
using DataAccess.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.FaqQuestion;

namespace Web.Areas.Admin.Services.Concrete
{
    public class FaqQuestionService :IFaqQuestionService
    {

        private readonly ModelStateDictionary _modelState;
        private readonly IFaqQuestionRepository _faqQuestionRepository;
        private readonly IFaqCategoryRepository _faqCategoryRepository;

        public FaqQuestionService(IFaqQuestionRepository faqQuestionRepository ,IFaqCategoryRepository faqCategoryRepository, IActionContextAccessor actionContextAccessor)
        {
            _modelState = actionContextAccessor.ActionContext.ModelState;
            _faqQuestionRepository = faqQuestionRepository;
            _faqCategoryRepository = faqCategoryRepository;
        }

        public async Task<FaqQuestionIndexVM> GetAllAsync()
        {
            var model = new FaqQuestionIndexVM
            {
                FaqQuestions = await _faqQuestionRepository.GetAllGetCategoryAsync()
            };
            return model;

        }

        public async Task<FaqQuestionCreateVM> GetCreateModelAsync()
        {
            var faqCategory = await _faqCategoryRepository.GetAllAsync();
            var model = new FaqQuestionCreateVM
            {
                FaqCategory = faqCategory.Select(c => new SelectListItem
                {
                    Text = c.Title,
                    Value = c.Id.ToString()
                }).ToList()
            };

            return model;
        }


        public async Task<bool> CreateAsync(FaqQuestionCreateVM model)
        {
            if (!_modelState.IsValid) return false;

            var isExist = await _faqQuestionRepository.AnyAsync(c => c.Title.Trim().ToLower() == model.Title.Trim().ToLower());
            if (isExist)
            {
                _modelState.AddModelError("Title", "Bu adda Question mövcuddur");
                return false;
            }


         

            var faqQuestion = new FaqQuestion
            {
                Title = model.Title,
                Description = model.Description,
                CreatedAt = DateTime.Now,
                FaqCategoryId = model.FaqCategoryId,


            };

            await _faqQuestionRepository.CreateAsync(faqQuestion);
            return true;
        }


        public async Task<FaqQuestionUpdateVM> GetUpdateModelAsync(int id)
        {


            var faqCategory = await _faqCategoryRepository.GetAllAsync();
            var faqQuestion = await _faqQuestionRepository.GetAsync(id);

            if (faqQuestion == null) return null;

            var model = new FaqQuestionUpdateVM
            {
                Id = faqQuestion.Id,
                FaqCategory = faqCategory.Select(c => new SelectListItem
                {
                    Text = c.Title,
                    Value = c.Id.ToString()
                }).ToList(),
                FaqCategoryId = faqQuestion.FaqCategoryId,
                Description = faqQuestion.Description,
                Title = faqQuestion.Title,

            };

            return model;

        }

        public async Task<bool> UpdateAsync(FaqQuestionUpdateVM model)
        {
            if (!_modelState.IsValid) return false;

            var isExist = await _faqQuestionRepository.AnyAsync(c => c.Title.Trim().ToLower() == model.Title.Trim().ToLower() && c.Id != model.Id);
            if (isExist)
            {
                _modelState.AddModelError("Title", "Bu adda kateqoriya mövcuddur");
                return false;
            }
           

            var faqQuestion = await _faqQuestionRepository.GetAsync(model.Id);

           

            


            if (faqQuestion != null)
            {
                faqQuestion.Title = model.Title;
                faqQuestion.ModifiedAt = DateTime.Now;
                faqQuestion.Description = model.Description;
                faqQuestion.FaqCategoryId = model.FaqCategoryId;
                await _faqQuestionRepository.UpdateAsync(faqQuestion);
            }
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var faqCategory = await _faqCategoryRepository.GetAsync(id);
            if (faqCategory != null)
            {

                await _faqCategoryRepository.DeleteAsync(faqCategory);

                return true;

            }

            return false;
        }
    }
}
