using Core.Entities;
using Core.Utilities.FileService;
using DataAccess.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.MedicalDepartament;

namespace Web.Areas.Admin.Services.Concrete
{
    public class MedicalDepartamentService : IMedicalDepartamentService
    {
        private readonly ModelStateDictionary _modelState;
        private readonly IMedicalDepartamentRepository _medicalDepartamentRepository;
        private readonly IFileService _fileService;


        public MedicalDepartamentService(IMedicalDepartamentRepository medicalDepartamentRepository, IActionContextAccessor actionContextAccessor, IFileService fileService)
        {
            _medicalDepartamentRepository = medicalDepartamentRepository;
            _fileService = fileService;
            _modelState = actionContextAccessor.ActionContext.ModelState;

        }

        public async Task<MedicalDepartamentIndexVM> GetAllAsync()
        {
            var model = new MedicalDepartamentIndexVM
            {
                MedicalDepartaments = await _medicalDepartamentRepository.GetAllAsync()
            };
            return model;

        }

        public async Task<bool> CreateAsync(MedicalDepartamentCreateVM model)
        {
            if (!_modelState.IsValid) return false;

            var isExist = await _medicalDepartamentRepository.AnyAsync(c => c.Title.Trim().ToLower() == model.Title.Trim().ToLower());
            if (isExist)
            {
                _modelState.AddModelError("Title", "Bu adda Departament mövcuddur");
                return false;
            }

            if (!_fileService.IsImage(model.MainPhoto))
            {
                _modelState.AddModelError("MainPhoto", "File image formatinda deyil zehmet olmasa image formasinda secin!!");
                return false;
            }
            if (!_fileService.CheckSize(model.MainPhoto, 300))
            {
                _modelState.AddModelError("MainPhoto", "File olcusu 300 kbdan boyukdur");
                return false;
            }



            var medicalDepartament = new MedicalDepartament
            {
                Id = model.Id,
                Title = model.Title,
                Description = model.Description,
                CreatedAt = DateTime.Now,
                PhotoName = await _fileService.UploadAsync(model.MainPhoto),
            };

            await _medicalDepartamentRepository.CreateAsync(medicalDepartament);
            return true;
        }

        public async Task<MedicalDepartamentUpdateVM> GetUpdateModelAsync(int id)
        {


            var medicalDepartament = await _medicalDepartamentRepository.GetAsync(id);

            if (medicalDepartament == null) return null;

            var model = new MedicalDepartamentUpdateVM
            {
                Id = medicalDepartament.Id,
                Title = medicalDepartament.Title,
                Description = medicalDepartament.Description,
                MainPhotoName = medicalDepartament.PhotoName,

            };

            return model;

        }


        public async Task<bool> UpdateAsync(MedicalDepartamentUpdateVM model)
        {
            if (!_modelState.IsValid) return false;

            var isExist = await _medicalDepartamentRepository.AnyAsync(c => c.Title.Trim().ToLower() == model.Title.Trim().ToLower() && c.Id != model.Id);
            if (isExist)
            {
                _modelState.AddModelError("Title", "Bu adda Departament mövcuddur");
                return false;
            }
            if (model.MainPhoto != null)
            {
                if (!_fileService.IsImage(model.MainPhoto))
                {
                    _modelState.AddModelError("MainPhoto", "File image formatinda deyil zehmet olmasa image formasinda secin!!");
                    return false;
                }
                if (!_fileService.CheckSize(model.MainPhoto, 300))
                {
                    _modelState.AddModelError("MainPhoto", "File olcusu 300 kbdan boyukdur");
                    return false;
                }
            }

            var medicalDepartament = await _medicalDepartamentRepository.GetAsync(model.Id);




            if (medicalDepartament != null)
            {
                medicalDepartament.Id = model.Id;
                medicalDepartament.Title = model.Title;
                medicalDepartament.ModifiedAt = DateTime.Now;
                medicalDepartament.Description = model.Description;



                if (model.MainPhoto != null)
                {
                    medicalDepartament.PhotoName = await _fileService.UploadAsync(model.MainPhoto);
                }

                await _medicalDepartamentRepository.UpdateAsync(medicalDepartament);

            }
            return true;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var medicalDepartament = await _medicalDepartamentRepository.GetAsync(id);
            if (medicalDepartament != null)
            {
                _fileService.Delete(medicalDepartament.PhotoName);




                await _medicalDepartamentRepository.DeleteAsync(medicalDepartament);

                return true;

            }

            return false;
        }

    }
}
