using Core.Entities;
using Core.Utilities.FileService;
using DataAccess.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.AboutHome;
using Web.Areas.Admin.ViewModels.AboutHome.AboutHomePhoto;

namespace Web.Areas.Admin.Services.Concrete
{
    public class AboutService :IAboutService
    {

        
        private readonly IAboutRepository _aboutRepository;
        private readonly IAboutPhotoRepository _aboutPhotoRepository;
        private readonly IFileService _fileService;
        private readonly ModelStateDictionary _modelState;

        public AboutService(IAboutRepository aboutRepository,IAboutPhotoRepository aboutPhotoRepository, IActionContextAccessor actionContextAccessor, IFileService fileService)
        {
            _aboutRepository = aboutRepository;
            _aboutPhotoRepository = aboutPhotoRepository;
            _fileService = fileService;
            _modelState = actionContextAccessor.ActionContext.ModelState;
        }

        #region About
        public async Task<AboutIndexVM> GetAsync()
        {
            var model = new AboutIndexVM
            {
                AboutHome = await _aboutRepository.GetWithPhotosAsync()
            };
            return model;

        }


        public async Task<bool> CreateAsync(AboutCreateVM model)
        {



            if (!_modelState.IsValid) return false;

            var isExist = await _aboutRepository.AnyAsync(c => c.Title.Trim().ToLower() == model.Title.Trim().ToLower());
            if (isExist)
            {
                _modelState.AddModelError("Title", "Bu adda kateqoriya mövcuddur");
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

            bool hasError = false;
            foreach (var photo in model.Photos)
            {
                if (!_fileService.IsImage(photo))
                {
                    _modelState.AddModelError("Photos", $"{photo.FileName} yuklediyiniz file sekil formatinda olmalidir");
                    hasError = true;

                }
                else if (!_fileService.CheckSize(photo, 300))
                {
                    _modelState.AddModelError("Photos", $"{photo.FileName} ci yuklediyiniz sekil 300 kb dan az olmalidir");
                    hasError = true;

                }

            }

            if (hasError) { return false; }

            var aboutHome = new About
            {
                Title = model.Title,
                Description = model.Description,
                CreatedAt = DateTime.Now,
                MainPhoto = await _fileService.UploadAsync(model.MainPhoto),


            };

            await _aboutRepository.CreateAsync(aboutHome);

            int order = 1;
            foreach (var photo in model.Photos)
            {
                var aboutHomePhoto = new AboutPhoto
                {
                    Name = await _fileService.UploadAsync(photo),
                    Order = order,
                    AboutHomeId = aboutHome.Id

                };
                await _aboutPhotoRepository.CreateAsync(aboutHomePhoto);

                order++;
            }


            return true;
        }

        public async Task<AboutUpdateVM> GetUpdateModelAsync(int id)
        {



            var aboutHome = await _aboutRepository.GetAsync(id);

            if (aboutHome == null) return null;

            var model = new AboutUpdateVM
            {
                Id = aboutHome.Id,
                Description = aboutHome.Description,
                Title = aboutHome.Title,
                MainPhotoPath = aboutHome.MainPhoto,
                aboutHomePhotos = await _aboutPhotoRepository.GetAllAsync(),

            };

            return model;

        }


        public async Task<AboutDetailsVM> GetDetailsModelAsync(int id)
        {



            var aboutHome = await _aboutRepository.GetAsync(id);

            if (aboutHome == null) return null;

            var model = new AboutDetailsVM
            {
                Id = aboutHome.Id,
                Description = aboutHome.Description,
                Title = aboutHome.Title,
                MainPhotoPath = aboutHome.MainPhoto,
                aboutHomePhotos = await _aboutPhotoRepository.GetAllAsync(),

            };

            return model;

        }

        public async Task<bool> UpdateAsync(AboutUpdateVM model)
        {
            if (!_modelState.IsValid) return false;

            var isExist = await _aboutRepository.AnyAsync(c => c.Title.Trim().ToLower() == model.Title.Trim().ToLower() && c.Id != model.Id);
            if (isExist)
            {
                _modelState.AddModelError("Title", "Bu adda kateqoriya mövcuddur");
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

            var aboutHome = await _aboutRepository.GetWithPhotosAsync(model.Id);

            bool hasError = false;

            if (model.Photos != null)
            {
                foreach (var photo in model.Photos)
                {
                    if (!_fileService.IsImage(photo))
                    {
                        _modelState.AddModelError("Photos", $"{photo.FileName} yuklediyiniz file sekil formatinda olmalidir");
                        hasError = true;
                    }
                    else if (!_fileService.CheckSize(photo, 300))
                    {
                        _modelState.AddModelError("Photos", $"{photo.FileName} ci yuklediyiniz sekil 300 kb dan az olmalidir");
                        hasError = true;
                    }
                }

                if (hasError) { return false; }

                int order = aboutHome.AboutPhotos.OrderByDescending(pp => pp.Order).FirstOrDefault().Order;
                foreach (var photo in model.Photos)
                {
                    var aboutHomePhoto = new AboutPhoto
                    {
                        Name = await _fileService.UploadAsync(photo),
                        Order = ++order,
                        AboutHomeId = aboutHome.Id
                    };
                    await _aboutPhotoRepository.CreateAsync(aboutHomePhoto);



                }
            }



            if (aboutHome != null)
            {
                aboutHome.Title = model.Title;
                aboutHome.ModifiedAt = DateTime.Now;
                aboutHome.Description = model.Description;


                if (model.MainPhoto != null)
                {
                    aboutHome.MainPhoto = await _fileService.UploadAsync(model.MainPhoto);
                }

                await _aboutRepository.UpdateAsync(aboutHome);

            }
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var aboutHome = await _aboutRepository.GetAsync(id);
            if (aboutHome != null)
            {
                _fileService.Delete(aboutHome.MainPhoto);


                foreach (var photo in await _aboutPhotoRepository.GetAllAsync())
                {
                    _fileService.Delete(photo.Name);

                }

                await _aboutRepository.DeleteAsync(aboutHome);

                return true;

            }

            return false;
        }


        public async Task<bool> DeletePhotoAsync(int id)
        {
            var aboutHomePhoto = await _aboutPhotoRepository.GetAsync(id);
            if (aboutHomePhoto != null)
            {
                _fileService.Delete(aboutHomePhoto.Name);




                await _aboutPhotoRepository.DeleteAsync(aboutHomePhoto);

                return true;

            }

            return false;
        }


        public async Task<AboutPhotoUpdateVM> GetPhotoUpdateModelAsync(int id)
        {



            var aboutHomePhoto = await _aboutPhotoRepository.GetAsync(id);

            if (aboutHomePhoto == null) return null;

            var model = new AboutPhotoUpdateVM
            {
                Id = id,
                Order = aboutHomePhoto.Order,
                AboutHomeId = aboutHomePhoto.AboutHomeId,
            };

            return model;

        }


        public async Task<bool> UpdatePhotoAsync(AboutPhotoUpdateVM model)
        {
            if (!_modelState.IsValid) return false;

            var aboutHomePhoto = await _aboutPhotoRepository.GetAsync(model.Id);

            if (aboutHomePhoto != null)
            {
                aboutHomePhoto.Order = model.Order;

                await _aboutPhotoRepository.UpdateAsync(aboutHomePhoto);

            }
            return true;
        }


        #endregion

    }
}
