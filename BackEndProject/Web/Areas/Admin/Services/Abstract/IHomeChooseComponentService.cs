using Web.Areas.Admin.ViewModels.HomeChooseComponent;

namespace Web.Areas.Admin.Services.Abstract
{
    public interface IHomeChooseComponentService
    {
        Task<HomeChooseComponentIndexVM> GetAsync();
        Task<bool> CreateAsync(HomeChooseComponentCreateVM model);

        Task<HomeChooseComponentUpdateVM> GetUpdateModelAsync(int id);
        Task<bool> UpdateAsync(HomeChooseComponentUpdateVM model);
        Task<bool> DeleteAsync(int id);
    }
}
