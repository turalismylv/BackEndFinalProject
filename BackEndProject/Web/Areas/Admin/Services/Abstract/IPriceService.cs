using Web.Areas.Admin.ViewModels.Price;

namespace Web.Areas.Admin.Services.Abstract
{
    public interface IPriceService
    {

        Task<PriceIndexVM> GetAllAsync();

        Task<bool> CreateAsync(PriceCreateVM model);
        Task<PriceUpdateVM> GetUpdateModelAsync(int id);
        Task<bool> UpdateAsync(PriceUpdateVM model);
        Task<bool> DeleteAsync(int id);
    }
}
