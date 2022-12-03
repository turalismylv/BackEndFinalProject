using Core.Entities;
using Core.Utilities.FileService;
using DataAccess.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.Doctor;

namespace Web.Areas.Admin.Services.Concrete
{
    public class DoctorService :IDoctorService
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly IFileService _fileService;
        private readonly ModelStateDictionary _modelState;

        public DoctorService(IDoctorRepository doctorRepository, IActionContextAccessor actionContextAccessor, IFileService fileService)
        {
            _doctorRepository = doctorRepository;
            _fileService = fileService;
            _modelState = actionContextAccessor.ActionContext.ModelState;
        }


        public async Task<DoctorIndexVM> GetAllAsync()
        {
            var model = new DoctorIndexVM
            {
                Doctors = await _doctorRepository.GetAllAsync()
            };
            return model;

        }

        public async Task<bool> CreateAsync(DoctorCreateVM model)
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



            var doctor = new Doctor
            {
                FullName = model.FullName,
                Description = model.Description,
                CreatedAt = DateTime.Now,
                Skill=model.Skill,
                Specialty=model.Specialty,
                HomePageSee=model.HomePageSee,
                Email=model.Email,
                PhoneNumber=model.PhoneNumber,
                FbUrl=model.FbUrl,
                LiUrl=model.LiUrl,
                TwUrl=model.TwUrl,
                WorkingTime=model.WorkingTime,
                Qualification=model.Qualification,
                MainPhoto = await _fileService.UploadAsync(model.MainPhoto),
            };

            await _doctorRepository.CreateAsync(doctor);
            return true;
        }
        public async Task<DoctorUpdateVM> GetUpdateModelAsync(int id)
        {


            var doctor = await _doctorRepository.GetAsync(id);

            if (doctor == null) return null;

            var model = new DoctorUpdateVM
            {
                Id = doctor.Id,
                FullName = doctor.FullName,
                Description = doctor.Description,
                MainPhotoName = doctor.MainPhoto,
                Skill = doctor.Skill,
                Specialty = doctor.Specialty,
                HomePageSee = doctor.HomePageSee,
                Email = doctor.Email,
                FbUrl = doctor.FbUrl,
                LiUrl = doctor.LiUrl,
                PhoneNumber = doctor.PhoneNumber,
                Qualification = doctor.Qualification,
                TwUrl = doctor.TwUrl,
                WorkingTime = doctor.WorkingTime,

            };

            return model;

        }
        public async Task<bool> UpdateAsync(DoctorUpdateVM model)
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

            var doctor = await _doctorRepository.GetAsync(model.Id);




            if (doctor != null)
            {
                doctor.Id = model.Id;
                doctor.FullName = model.FullName;
                doctor.ModifiedAt = DateTime.Now;
                doctor.Description = model.Description;
                doctor.Qualification = model.Qualification;
                doctor.LiUrl = model.LiUrl;
                doctor.TwUrl = model.TwUrl;
                doctor.FbUrl = model.FbUrl;
                doctor.Email = model.Email;
                doctor.HomePageSee = model.HomePageSee;
                doctor.PhoneNumber = model.PhoneNumber;
                doctor.Skill = model.Skill;
                doctor.Specialty = model.Specialty;
                doctor.WorkingTime = model.WorkingTime;

                if (model.MainPhoto != null)
                {
                    doctor.MainPhoto = await _fileService.UploadAsync(model.MainPhoto);
                }

                await _doctorRepository.UpdateAsync(doctor);

            }
            return true;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var doctor = await _doctorRepository.GetAsync(id);
            if (doctor != null)
            {
                _fileService.Delete(doctor.MainPhoto);




                await _doctorRepository.DeleteAsync(doctor);

                return true;

            }

            return false;
        }


    }
}
