using Core.Entities;
using DataAccess.Contexts;
using DataAccess.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Concrete
{
    public class BasketProductRepository : Repository<BasketProduct>, IBasketProductRepository
    {
        private readonly AppDbContext _context;

        public BasketProductRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<BasketProduct> GetBasketProduct(int productId,int basketId)
        {
            var basketProduct = await _context.BasketProducts.FirstOrDefaultAsync(bp => bp.ProductId == productId && bp.BasketId == basketId);

            return basketProduct;
        }

    }

}
