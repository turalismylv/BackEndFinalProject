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
    public class HomeChooseComponentRepository : Repository<HomeChooseComponent>, IHomeChooseComponentRepository
    {
        private readonly AppDbContext _context;

        public HomeChooseComponentRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

       



        public async Task<HomeChooseComponent> GetAsync()
        {
            return await _context.HomeChooseComponent.FirstOrDefaultAsync();
        }

    }
   
}
