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
    public class LastestNewRepository : Repository<LastestNew>, ILastestNewRepository
    {
        private readonly AppDbContext _context;

        public LastestNewRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }


        public async Task<List<LastestNew>> GetOrderByAsync()
        {
            return await _context.LastestNews.OrderByDescending(lw=>lw.Id).ToListAsync();
        }

    }
   
}
