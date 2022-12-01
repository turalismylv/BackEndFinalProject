using Web.ViewModels.MedicalDepartament;

namespace Web.Services.Abstract
{
    public interface IMedicalDepartamentService
    {
        Task<MedicalDepartamentIndexVM> GetAllAsync();
    }
}
