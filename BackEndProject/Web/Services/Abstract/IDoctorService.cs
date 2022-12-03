using Core.Entities;
using Web.ViewModels.Doctor;

namespace Web.Services.Abstract
{
    public interface IDoctorService
    {
        Task<DoctorIndexVM> GetAllAsync(DoctorIndexVM model);

        Task<DoctorDetailsVM> GetAsync(int id);

        IQueryable<Doctor> FilterDoctors(DoctorIndexVM model);

        Task<List<Doctor>> PaginateDoctorsAsync(int page, int take, DoctorIndexVM model);
    }
}
