using Web.Areas.Admin.ViewModels.AboutHome;
using Web.Areas.Admin.ViewModels.AboutHome.AboutHomePhoto;

namespace Web.Areas.Admin.Services.Abstract
{
    public interface IAboutService
    {

        Task<AboutIndexVM> GetAsync();
        Task<bool> CreateAsync(AboutCreateVM model);
        Task<AboutUpdateVM> GetUpdateModelAsync(int id);

        Task<bool> UpdateAsync(AboutUpdateVM model);
        Task<bool> DeleteAsync(int id);
        Task<bool> DeletePhotoAsync(int id);
        Task<AboutPhotoUpdateVM> GetPhotoUpdateModelAsync(int id);
        Task<bool> UpdatePhotoAsync(AboutPhotoUpdateVM model);
    }
}
