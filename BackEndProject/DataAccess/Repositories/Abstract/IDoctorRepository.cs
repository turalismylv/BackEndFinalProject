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
    }
}
