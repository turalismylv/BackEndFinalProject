using Core.Entities;
using Core.Utilities.FileService;
using DataAccess.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.HomeChooseComponent;

namespace Web.Areas.Admin.Services.Concrete
{
    public class HomeChooseComponentService :IHomeChooseComponentService
    {

        private readonly IHomeChooseComponentRepository _homeChooseComponentRepository;
        private readonly IFileService _fileService;
        private readonly ModelStateDictionary _modelState;

        public HomeChooseComponentService(IHomeChooseComponentRepository homeChooseComponentRepository, IActionContextAccessor actionContextAccessor, IFileService fileService)
        {
            _homeChooseComponentRepository = homeChooseComponentRepository;
            _fileService = fileService;
            _modelState = actionContextAccessor.ActionContext.ModelState;
        }


        public async Task<HomeChooseComponentIndexVM> GetAsync()
        {
            var model = new HomeChooseComponentIndexVM
            {
                HomeChooseComponent = await _homeChooseComponentRepository.GetAsync()
            };
            return model;
        }

        public async Task<bool> CreateAsync(HomeChooseComponentCreateVM model)
        {
            if (!_modelState.IsValid) return false;

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

            var homeChooseComponent = new HomeChooseComponent
            {
                Title = model.Title,
                Description = model.Description,
                CreatedAt = DateTime.Now,
                PhotoName = await _fileService.UploadAsync(model.MainPhoto),
                Doctor=model.Doctor,
                Experience=model.Experience,
                Patient=model.Patient,
                Quality=model.Quality,
                Text=model.Text,
            };

            await _homeChooseComponentRepository.CreateAsync(homeChooseComponent);

            return true;
        }

        public async Task<HomeChooseComponentUpdateVM> GetUpdateModelAsync(int id)
        {
            var homeChooseComponent = await _homeChooseComponentRepository.GetAsync(id);

            if (homeChooseComponent == null) return null;

            var model = new HomeChooseComponentUpdateVM
            {
                Id = homeChooseComponent.Id,
                Description = homeChooseComponent.Description,
                Title = homeChooseComponent.Title,
                MainPhotoPath = homeChooseComponent.PhotoName,
                Doctor=homeChooseComponent.Doctor,
                Experience= homeChooseComponent.Experience,
                Patient=homeChooseComponent.Patient,
                Quality=homeChooseComponent.Quality,
                Text = homeChooseComponent.Text,
               
            };

            return model;

        }


        public async Task<bool> UpdateAsync(HomeChooseComponentUpdateVM model)
        {
            if (!_modelState.IsValid) return false;

            
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

            var homeChooseComponent = await _homeChooseComponentRepository.GetAsync(model.Id);
            if (homeChooseComponent != null)
            {
                homeChooseComponent.Title = model.Title;
                homeChooseComponent.ModifiedAt = DateTime.Now;
                homeChooseComponent.Description = model.Description;
                homeChooseComponent.Patient = model.Patient;
                homeChooseComponent.Experience = model.Experience;
                homeChooseComponent.Quality = model.Quality;
                homeChooseComponent.Doctor = model.Doctor;
                homeChooseComponent.Text = model.Text;
                


                if (model.MainPhoto != null)
                {
                    homeChooseComponent.PhotoName = await _fileService.UploadAsync(model.MainPhoto);
                }

                await _homeChooseComponentRepository.UpdateAsync(homeChooseComponent);

            }
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var homeChooseComponent = await _homeChooseComponentRepository.GetAsync(id);
            if (homeChooseComponent != null)
            {
                _fileService.Delete(homeChooseComponent.PhotoName);
                await _homeChooseComponentRepository.DeleteAsync(homeChooseComponent);

                return true;

            }

            return false;
        }

    }
}
