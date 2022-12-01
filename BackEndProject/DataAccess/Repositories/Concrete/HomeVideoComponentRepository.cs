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
    public class HomeVideoComponentRepository : Repository<HomeVideoComponent>, IHomeVideoComponentRepository
    {
        private readonly AppDbContext _context;

        public HomeVideoComponentRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<HomeVideoComponent> GetAsync()
        {
            return await _context.HomeVideoComponent.FirstOrDefaultAsync();
        }
    }



}
