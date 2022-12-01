using Web.Areas.Admin.ViewModels.HomeVideoComponent;

namespace Web.Areas.Admin.Services.Abstract
{
    public interface IHomeVideoComponentService
    {
        Task<HomeVideoComponentIndexVM> GetAsync();

        Task<bool> CreateAsync(HomeVideoComponentCreateVM model);
        Task<HomeVideoComponentUpdateVM> GetUpdateModelAsync(int id);
        Task<bool> UpdateAsync(HomeVideoComponentUpdateVM model);
        Task<bool> DeleteAsync(int id);
    }
}
