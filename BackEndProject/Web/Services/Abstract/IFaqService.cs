using Web.ViewModels.Faq;

namespace Web.Services.Abstract
{
    public interface IFaqService
    {

        Task<FaqCategoryIndexVM> GetAllAsync();
        Task<FaqQuestionIndexVM> CategoryQuestionAsync(int id);
    }
}
