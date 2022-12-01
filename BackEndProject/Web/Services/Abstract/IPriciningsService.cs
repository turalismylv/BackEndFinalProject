using Web.ViewModels.Price;

namespace Web.Services.Abstract
{
    public interface IPriciningsService
    {
        Task<PriceIndexVM> GetAllAsync();
    }
}
