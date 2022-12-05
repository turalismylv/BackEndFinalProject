using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Abstract
{
    public interface IDoctorRepository : IRepository<Doctor>
    {

        Task<int> GetPageCountAsync(int take);

        IQueryable<Doctor> FilterByTitle(string fullName);
        Task<List<Doctor>> PaginateDoctorsAsync(IQueryable<Doctor> doctors, int page, int take);
        Task<List<Doctor>> Filter(string fullName, int page, int take);
        Task<List<Doctor>> GetHomeDoctorAsync();


        }
}
