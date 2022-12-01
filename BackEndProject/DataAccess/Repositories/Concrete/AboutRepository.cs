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
    public class AboutRepository : Repository<About>, IAboutRepository
    {
        private readonly AppDbContext _context;

        public AboutRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<About> GetWithPhotosAsync(int id)
        {
            return await _context.About.Include(p=>p.AboutPhotos).FirstOrDefaultAsync(p => p.Id == id);
        }

       

        public async Task<About> GetWithPhotosAsync()
        {
            return await _context.About.Include(p => p.AboutPhotos).FirstOrDefaultAsync();
        }
    }
   
}
