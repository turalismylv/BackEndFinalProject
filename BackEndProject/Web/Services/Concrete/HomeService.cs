
using DataAccess.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Web.Services.Abstract;
using Web.ViewModels.Home;

namespace Web.Services.Concrete
{
    public class HomeService :IHomeService
    {
        private readonly ILastestNewRepository _lastestNewRepository;
        private readonly IHomeChooseComponentRepository _homeChooseComponentRepository;
        private readonly IHomeVideoComponentRepository _homeVideoComponentRepository;
        private readonly IHomeMainSliderRepository _homeMainSliderRepository;
        private readonly IAboutRepository _aboutRepository;
        private readonly IOurVisionRepository _ourVisionRepository;
        private readonly IMedicalDepartamentRepository _medicalDepartamentRepository;
        private readonly ModelStateDictionary _modelState;

        public HomeService(ILastestNewRepository lastestNewRepository,IHomeChooseComponentRepository homeChooseComponentRepository,IHomeVideoComponentRepository homeVideoComponentRepository ,IHomeMainSliderRepository homeMainSliderRepository,IAboutRepository aboutRepository,IOurVisionRepository ourVisionRepository, IActionContextAccessor actionContextAccessor,IMedicalDepartamentRepository medicalDepartamentRepository)
        {
            _lastestNewRepository = lastestNewRepository;
            _homeChooseComponentRepository = homeChooseComponentRepository;
            _homeVideoComponentRepository = homeVideoComponentRepository;
            _homeMainSliderRepository = homeMainSliderRepository;
            _aboutRepository = aboutRepository;
            _ourVisionRepository = ourVisionRepository;
            _medicalDepartamentRepository = medicalDepartamentRepository;
            _modelState = actionContextAccessor.ActionContext.ModelState;
        }


        public async Task<HomeIndexVM> GetAllAsync()
        {
            var model = new HomeIndexVM
            {
                HomeMainSliders = await _homeMainSliderRepository.GetAllAsync(),
                ourVisions=await _ourVisionRepository.GetAllAsync(),
                medicalDepartaments = await _medicalDepartamentRepository.GetAllAsync(),
                About=await _aboutRepository.GetWithPhotosAsync(),
                homeVideoComponent=await _homeVideoComponentRepository.GetAsync(),
                HomeChooseComponent=await _homeChooseComponentRepository.GetAsync(),
                LastestNews=await _lastestNewRepository.GetOrderByAsync()
            };
            return model;

        }
    }
}
