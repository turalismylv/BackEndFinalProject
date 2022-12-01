using Web.Areas.Admin.ViewModels.FaqCategory;

namespace Web.Areas.Admin.Services.Abstract
{
    public interface IFaqCategoryService
    {

        Task<FaqCategoryIndexVM> GetAllAsync();
        Task<bool> CreateAsync(FaqCategoryCreateVM model);
        Task<FaqCategoryUpdateVM> GetUpdateModelAsync(int id);
        Task<bool> UpdateAsync(FaqCategoryUpdateVM model);
        Task<bool> DeleteAsync(int id);
    }
}
