using Web.ViewModels.Doctor;

namespace Web.Services.Abstract
{
    public interface IDoctorService
    {
        Task<DoctorIndexVM> GetAllAsync();

        Task<DoctorDetailsVM> GetAsync(int id);
    }
}
