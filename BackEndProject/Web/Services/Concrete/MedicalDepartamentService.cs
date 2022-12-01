using DataAccess.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Web.Services.Abstract;
using Web.ViewModels.MedicalDepartament;

namespace Web.Services.Concrete
{
    public class MedicalDepartamentService :IMedicalDepartamentService
    {
     
        private readonly ModelStateDictionary _modelState;
        private readonly IMedicalDepartamentRepository _medicalDepartamentRepository;

        public MedicalDepartamentService(IActionContextAccessor actionContextAccessor,IMedicalDepartamentRepository medicalDepartamentRepository)
        {
            _modelState = actionContextAccessor.ActionContext.ModelState;
            _medicalDepartamentRepository = medicalDepartamentRepository;
        }


        public async Task<MedicalDepartamentIndexVM> GetAllAsync()
        {
            var model = new MedicalDepartamentIndexVM
            {
                medicalDepartaments = await _medicalDepartamentRepository.GetAllAsync(),
             
            };
            return model;

        }
    }
}
