using Core.Entities;
using System.Security.Claims;
using Web.ViewModels.Basket;

namespace Web.Services.Abstract
{
    public interface IBasketService
    {
        Task<BasketIndexVM> GetAsync(ClaimsPrincipal user);

        Task<bool> Add(BasketAddVM model, ClaimsPrincipal userClaims);
        Task<bool> DeleteBasketProduct(int productId, ClaimsPrincipal userClaims);

        Task<bool> UpCount(int productId, ClaimsPrincipal userClaims);
        Task<bool> DownCount(int productId, ClaimsPrincipal userClaims);

        Task<bool> ClearBasketProduct( ClaimsPrincipal userClaims);
    }
}
