using Core.Entities;
using DataAccess.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Web.Services.Abstract;
using Web.ViewModels.Doctor;

namespace Web.Services.Concrete
{
    public class DoctorService : IDoctorService
    {
        private readonly ModelStateDictionary _modelState;
        private readonly IDoctorRepository _doctorRepository;

        public DoctorService(IDoctorRepository doctorRepository, IActionContextAccessor actionContextAccessor)
        {
            _modelState = actionContextAccessor.ActionContext.ModelState;
            _doctorRepository = doctorRepository;
        }


        public async Task<DoctorIndexVM> GetAllAsync(DoctorIndexVM model)
        {
            var pageCount = await _doctorRepository.GetPageCountAsync(model.Take,model.FullName);

            if (model.Page <= 0 /*|| model.Page > pageCount*/) return model;

            var doctors = await _doctorRepository.Filter(model.FullName,model.Page,model.Take);

            model = new DoctorIndexVM
            {
                Doctors = doctors,
                Page = model.Page,
                PageCount = pageCount,
                Take = model.Take,
                FullName=model.FullName
                
                

            };
            return model;

        }

        public async Task<DoctorDetailsVM> GetAsync(int id)
        {
            var doctor = await _doctorRepository.GetAsync(id);

            if (doctor == null) return null;

            var model = new DoctorDetailsVM
            {
                Id = doctor.Id,
                FullName = doctor.FullName,
                Description = doctor.Description,
                MainPhoto = doctor.MainPhoto,
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


    }
}
