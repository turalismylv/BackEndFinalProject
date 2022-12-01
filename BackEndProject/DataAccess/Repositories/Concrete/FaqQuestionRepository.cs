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
    public class FaqQuestionRepository : Repository<FaqQuestion>, IFaqQuestionRepository
    {
        private readonly AppDbContext _context;

        public FaqQuestionRepository(AppDbContext context) : base(context) 
        {
            _context = context;
        }

        public async Task<List<FaqQuestion>> GetAllGetCategoryAsync()
        {
            return await _context.FaqQuestions.Include(p => p.FaqCategory).ToListAsync();
        }

        public async Task<List<FaqQuestion>> GetByCategoryIdAsync(int categoryId)
        {
            return await _context.FaqQuestions.Where(p => p.FaqCategoryId == categoryId).ToListAsync();
        }


    }
   
}
