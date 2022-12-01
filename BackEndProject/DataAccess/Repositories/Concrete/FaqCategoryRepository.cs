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
    public class FaqCategoryRepository : Repository<FaqCategory>, IFaqCategoryRepository
    {
        private readonly AppDbContext _context;

        public FaqCategoryRepository(AppDbContext context) : base(context) 
        {
            _context = context;
        }

        public async Task<FaqCategory> GetFirstAsync()
        {
            return await _context.FaqCategories.FirstOrDefaultAsync();
        }

    }
}
