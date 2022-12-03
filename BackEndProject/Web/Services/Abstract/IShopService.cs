using Web.ViewModels.Shop;

namespace Web.Services.Abstract
{
    public interface IShopService
    {
        Task<ShopIndexVM> GetAllAsync();
        Task<ShopProductIndexVM> CategoryProductAsync(int id);
    }
}
