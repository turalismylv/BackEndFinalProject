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
    public class DoctorRepository : Repository<Doctor>, IDoctorRepository
    {

        private readonly AppDbContext _context;

        public DoctorRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

     

        public async Task<int> GetPageCountAsync(int take)
        {
            var blogsCount = await _context.Doctors.CountAsync();
            return (int)Math.Ceiling((decimal)blogsCount / take);

        }

        public IQueryable<Doctor> FilterByTitle(string fullName)
        {
            return _context.Doctors.Where(p => !string.IsNullOrEmpty(fullName) ? p.FullName.Contains(fullName) : true);
        }


    }
}
