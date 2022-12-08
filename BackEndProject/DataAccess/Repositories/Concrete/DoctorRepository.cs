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

     

        public async Task<int> GetPageCountAsync(int take,string fullName)
        {
            var doctors = FilterByTitle(fullName);
            var pagecount = await doctors.CountAsync();
            return (int)Math.Ceiling((decimal)pagecount / take);

        }

        public async Task<List<Doctor>> Filter(string fullName,int page,int take)
        {
            var doctors=FilterByTitle(fullName);
            return await PaginateDoctorsAsync(doctors, page, take);
        }


        public IQueryable<Doctor> FilterByTitle(string fullName)
        {
            return _context.Doctors.Where(p => !string.IsNullOrEmpty(fullName) ? p.FullName.Contains(fullName) : true);
        }

        public async Task<List<Doctor>> PaginateDoctorsAsync(IQueryable<Doctor> doctors, int page, int take)
        {
            return await doctors
                 .OrderByDescending(b => b.Id)
                 .Skip((page - 1) * take).Take(take)
                 .ToListAsync();
        }

        public async Task<List<Doctor>> GetHomeDoctorAsync()
        {
            return await _context.Doctors.Where(d=>d.HomePageSee).ToListAsync();
        }
    }
}
