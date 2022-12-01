using Web.Areas.Admin.ViewModels.LastestNew;

namespace Web.Areas.Admin.Services.Abstract
{
    public interface ILastestNewService
    {
         Task<LastestNewIndexVM> GetAllAsync();

        Task<bool> CreateAsync(LastestNewCreateVM model);
        Task<LastestNewUpdateVM> GetUpdateModelAsync(int id);
        Task<bool> UpdateAsync(LastestNewUpdateVM model);
        Task<bool> DeleteAsync(int id);
    }
}
