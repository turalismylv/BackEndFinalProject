using Core.Entities;
using DataAccess.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Web.Services.Abstract;
using Web.ViewModels.Faq;

namespace Web.Services.Concrete
{
    public class FaqService :IFaqService
    {

        private readonly ModelStateDictionary _modelState;
        private readonly IFaqQuestionRepository _faqQuestionRepository;
        private readonly IFaqCategoryRepository _faqCategoryRepository;

        public FaqService(IFaqQuestionRepository faqQuestionRepository,IActionContextAccessor actionContextAccessor, IFaqCategoryRepository faqCategoryRepository)
        {
            _modelState = actionContextAccessor.ActionContext.ModelState;
            _faqQuestionRepository = faqQuestionRepository;
            _faqCategoryRepository = faqCategoryRepository;
        }


        public async Task<FaqCategoryIndexVM> GetAllAsync()
        {
            var category= await _faqCategoryRepository.GetFirstAsync();

            var model = new FaqCategoryIndexVM
            {
                FaqCategories = await _faqCategoryRepository.GetAllAsync(),
                FaqQuestions = category != null ? await _faqQuestionRepository.GetByCategoryIdAsync(category.Id) : new List<FaqQuestion>()
            };
            return model;

        }


        public async Task<FaqQuestionIndexVM> CategoryQuestionAsync(int id)
        {
            var category = await _faqCategoryRepository.GetAsync(id);
            var model = new FaqQuestionIndexVM
            {
                FaqCategorie = category,
                FaqQuestions = category != null ? await _faqQuestionRepository.GetByCategoryIdAsync(category.Id) : new List<FaqQuestion>()
            };

            return model;

        }
    }
}
