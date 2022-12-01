using Core.Entities;
using Core.Utilities.FileService;
using DataAccess.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.HomeVideoComponent;

namespace Web.Areas.Admin.Services.Concrete
{
    public class HomeVideoComponentService :IHomeVideoComponentService
    {
        private readonly IHomeVideoComponentRepository _homeVideoComponentRepository;
        private readonly IFileService _fileService;
        private readonly ModelStateDictionary _modelState;

        public HomeVideoComponentService(IHomeVideoComponentRepository homeVideoComponentRepository,IActionContextAccessor actionContextAccessor, IFileService fileService)
        {
            _homeVideoComponentRepository = homeVideoComponentRepository;
            _fileService = fileService;
            _modelState = actionContextAccessor.ActionContext.ModelState;
        }


        public async Task<HomeVideoComponentIndexVM> GetAsync()
        {
            var model = new HomeVideoComponentIndexVM
            {
                HomeVideoComponent = await _homeVideoComponentRepository.GetAsync()
            };
            return model;

        }

        public async Task<bool> CreateAsync(HomeVideoComponentCreateVM model)
        {

            if (!_modelState.IsValid) return false;

            if (!_fileService.IsImage(model.CoverPhoto))
            {
                _modelState.AddModelError("CoverPhoto", "File image formatinda deyil zehmet olmasa image formasinda secin!!");
                return false;
            }
            if (!_fileService.CheckSize(model.CoverPhoto, 300))
            {
                _modelState.AddModelError("CoverPhoto", "File olcusu 300 kbdan boyukdur");
                return false;
            }
            var homeVideoComponent = new HomeVideoComponent
            {
                CreatedAt = DateTime.Now,
                CoverImg = await _fileService.UploadAsync(model.CoverPhoto),
                VideoUrl=model.VideoUrl


            };
            await _homeVideoComponentRepository.CreateAsync(homeVideoComponent);
            return true;
        }


        public async Task<HomeVideoComponentUpdateVM> GetUpdateModelAsync(int id)
        {



            var homeVideoComponent = await _homeVideoComponentRepository.GetAsync(id);

            if (homeVideoComponent == null) return null;

            var model = new HomeVideoComponentUpdateVM
            {
                Id = homeVideoComponent.Id,
                MainPhotoPath = homeVideoComponent.CoverImg,
                VideoUrl=homeVideoComponent.VideoUrl
            };

            return model;

        }
        public async Task<bool> UpdateAsync(HomeVideoComponentUpdateVM model)
        {
            if (!_modelState.IsValid) return false;

        
            if (model.CoverPhoto != null)
            {
                if (!_fileService.IsImage(model.CoverPhoto))
                {
                    _modelState.AddModelError("CoverPhoto", "File image formatinda deyil zehmet olmasa image formasinda secin!!");
                    return false;
                }
                if (!_fileService.CheckSize(model.CoverPhoto, 2000))
                {
                    _modelState.AddModelError("CoverPhoto", "File olcusu 2000 kbdan boyukdur");
                    return false;
                }
            }

            var homeVideoComponent = await _homeVideoComponentRepository.GetAsync(model.Id);

     



            if (homeVideoComponent != null)
            {
                homeVideoComponent.Id = model.Id;
                homeVideoComponent.ModifiedAt = DateTime.Now;
                homeVideoComponent.VideoUrl = model.VideoUrl;


                if (model.CoverPhoto != null)
                {
                    homeVideoComponent.CoverImg = await _fileService.UploadAsync(model.CoverPhoto);
                }

                await _homeVideoComponentRepository.UpdateAsync(homeVideoComponent);

            }
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var homeVideoComponent = await _homeVideoComponentRepository.GetAsync(id);
            if (homeVideoComponent != null)
            {
                _fileService.Delete(homeVideoComponent.CoverImg);
                await _homeVideoComponentRepository.DeleteAsync(homeVideoComponent);
                return true;
            }

            return false;
        }

    }
}
