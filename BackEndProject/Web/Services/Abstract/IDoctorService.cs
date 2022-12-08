using Core.Entities;
using Web.ViewModels.Doctor;

namespace Web.Services.Abstract
{
    public interface IDoctorService
    {
        Task<DoctorIndexVM> GetAllAsync(DoctorIndexVM model);


        Task<DoctorDetailsVM> GetAsync(int id);

        
    }
}
