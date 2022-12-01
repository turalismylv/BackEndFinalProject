using Web.Areas.Admin.ViewModels.MedicalDepartament;

namespace Web.Areas.Admin.Services.Abstract
{
    public interface IMedicalDepartamentService
    {

        Task<MedicalDepartamentIndexVM> GetAllAsync();
        Task<bool> CreateAsync(MedicalDepartamentCreateVM model);

        Task<MedicalDepartamentUpdateVM> GetUpdateModelAsync(int id);

        Task<bool> UpdateAsync(MedicalDepartamentUpdateVM model);

        Task<bool> DeleteAsync(int id);
    }
}
