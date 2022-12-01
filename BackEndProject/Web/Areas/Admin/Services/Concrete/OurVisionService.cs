using Core.Entities;
using Core.Utilities.FileService;
using DataAccess.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.OurVision;

namespace Web.Areas.Admin.Services.Concrete
{
    public class OurVisionService :IOurVisionService
    {
        private readonly IOurVisionRepository _ourVisionRepository;
        private readonly IFileService _fileService;
        private readonly ModelStateDictionary _modelState;

        public OurVisionService(IOurVisionRepository ourVisionRepository, IActionContextAccessor actionContextAccessor, IFileService fileService)
        {
            _ourVisionRepository = ourVisionRepository;
           
            _fileService = fileService;
            _modelState = actionContextAccessor.ActionContext.ModelState;
        }

        #region OurVisionCrud
        public async Task<OurVisionIndexVM> GetAllAsync()
        {
            var model = new OurVisionIndexVM
            {
                OurVisions = await _ourVisionRepository.GetAllAsync()
            };
            return model;

        }

        public async Task<bool> CreateAsync(OurVisionCreateVM model)
        {
            if (!_modelState.IsValid) return false;

            var isExist = await _ourVisionRepository.AnyAsync(c => c.Title.Trim().ToLower() == model.Title.Trim().ToLower());
            if (isExist)
            {
                _modelState.AddModelError("Title", "Bu adda ourvision mövcuddur");
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



            var ourVision = new OurVision
            {
                Id = model.Id,
                Title = model.Title,
                Description = model.Description,
                CreatedAt = DateTime.Now,

                PhotoName = await _fileService.UploadAsync(model.MainPhoto),
            };

            await _ourVisionRepository.CreateAsync(ourVision);
            return true;
        }

        public async Task<OurVisionUpdateVM> GetUpdateModelAsync(int id)
        {


            var ourVision = await _ourVisionRepository.GetAsync(id);

            if (ourVision == null) return null;

            var model = new OurVisionUpdateVM
            {
                Id = ourVision.Id,
                Title = ourVision.Title,
                Description = ourVision.Description,
                MainPhotoName = ourVision.PhotoName,

            };

            return model;

        }


        public async Task<bool> UpdateAsync(OurVisionUpdateVM model)
        {
            if (!_modelState.IsValid) return false;

            var isExist = await _ourVisionRepository.AnyAsync(c => c.Title.Trim().ToLower() == model.Title.Trim().ToLower() && c.Id != model.Id);
            if (isExist)
            {
                _modelState.AddModelError("Title", "Bu adda ourvision mövcuddur");
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

            var ourVision = await _ourVisionRepository.GetAsync(model.Id);




            if (ourVision != null)
            {
                ourVision.Id = model.Id;
                ourVision.Title = model.Title;
                ourVision.ModifiedAt = DateTime.Now;
                ourVision.Description = model.Description;



                if (model.MainPhoto != null)
                {
                    ourVision.PhotoName = await _fileService.UploadAsync(model.MainPhoto);
                }

                await _ourVisionRepository.UpdateAsync(ourVision);

            }
            return true;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var ourVision = await _ourVisionRepository.GetAsync(id);
            if (ourVision != null)
            {
                _fileService.Delete(ourVision.PhotoName);




                await _ourVisionRepository.DeleteAsync(ourVision);

                return true;

            }

            return false;
        }


        #endregion
    }
}
