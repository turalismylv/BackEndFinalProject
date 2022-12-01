using Core.Entities;
using Core.Utilities.FileService;
using DataAccess.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.LastestNew;

namespace Web.Areas.Admin.Services.Concrete
{
    public class LastestNewService :ILastestNewService
    {
        private readonly ILastestNewRepository _lastestNewRepository;
        private readonly IFileService _fileService;
        private readonly ModelStateDictionary _modelState;

        public LastestNewService(ILastestNewRepository lastestNewRepository, IActionContextAccessor actionContextAccessor, IFileService fileService)
        {
            _lastestNewRepository = lastestNewRepository;
            _fileService = fileService;
            _modelState = actionContextAccessor.ActionContext.ModelState;
        }

        public async Task<LastestNewIndexVM> GetAllAsync()
        {
            var model = new LastestNewIndexVM
            {
                LastestNews = await _lastestNewRepository.GetAllAsync()
            };
            return model;

        }

        public async Task<bool> CreateAsync(LastestNewCreateVM model)
        {
            if (!_modelState.IsValid) return false;

            var isExist = await _lastestNewRepository.AnyAsync(c => c.Title.Trim().ToLower() == model.Title.Trim().ToLower());
            if (isExist)
            {
                _modelState.AddModelError("Title", "Bu adda news mövcuddur");
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



            var lastestNew = new LastestNew
            {
              
                Title = model.Title,
                Text = model.Text,
                Time=DateTime.Now,
                CreatedAt = DateTime.Now,
              
                PhotoName = await _fileService.UploadAsync(model.MainPhoto),
            };

            await _lastestNewRepository.CreateAsync(lastestNew);
            return true;
        }

        public async Task<LastestNewUpdateVM> GetUpdateModelAsync(int id)
        {


            var lastestNew = await _lastestNewRepository.GetAsync(id);

            if (lastestNew == null) return null;

            var model = new LastestNewUpdateVM
            {
                Id = lastestNew.Id,
                Title = lastestNew.Title,
                Text = lastestNew.Text,
                MainPhotoName = lastestNew.PhotoName,
                Time = lastestNew.Time,
            };

            return model;

        }

        public async Task<bool> UpdateAsync(LastestNewUpdateVM model)
        {
            if (!_modelState.IsValid) return false;

            var isExist = await _lastestNewRepository.AnyAsync(c => c.Title.Trim().ToLower() == model.Title.Trim().ToLower() && c.Id != model.Id);
            if (isExist)
            {
                _modelState.AddModelError("Title", "Bu adda news mövcuddur");
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

            var lastestNew = await _lastestNewRepository.GetAsync(model.Id);




            if (lastestNew != null)
            {
                lastestNew.Id = model.Id;
                lastestNew.Title = model.Title;
                lastestNew.ModifiedAt = DateTime.Now;
                lastestNew.Text = model.Text;
                lastestNew.Time = model.Time;


                if (model.MainPhoto != null)
                {
                    lastestNew.PhotoName = await _fileService.UploadAsync(model.MainPhoto);
                }

                await _lastestNewRepository.UpdateAsync(lastestNew);

            }
            return true;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var lastestNew = await _lastestNewRepository.GetAsync(id);
            if (lastestNew != null)
            {
                _fileService.Delete(lastestNew.PhotoName);




                await _lastestNewRepository.DeleteAsync(lastestNew);

                return true;

            }

            return false;
        }
    }
}
