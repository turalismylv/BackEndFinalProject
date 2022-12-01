using Web.Areas.Admin.ViewModels.FaqQuestion;

namespace Web.Areas.Admin.Services.Abstract
{
    public interface IFaqQuestionService
    {
        Task<FaqQuestionIndexVM> GetAllAsync();
        Task<FaqQuestionCreateVM> GetCreateModelAsync();
        Task<bool> CreateAsync(FaqQuestionCreateVM model);
        Task<FaqQuestionUpdateVM> GetUpdateModelAsync(int id);
        Task<bool> UpdateAsync(FaqQuestionUpdateVM model);
        Task<bool> DeleteAsync(int id);
    }
}
