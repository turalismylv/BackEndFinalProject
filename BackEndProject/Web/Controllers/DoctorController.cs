using Microsoft.AspNetCore.Mvc;
using Web.Services.Abstract;
using Web.ViewModels.Doctor;

namespace Web.Controllers
{
    public class DoctorController : Controller
    {
        private readonly IDoctorService _doctorService;

        public DoctorController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }
        [HttpGet]
        public async Task<IActionResult> Index(DoctorIndexVM model)
        {


            model = await _doctorService.GetAllAsync(model);
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var model = await _doctorService.GetAsync(id);
            return View(model);
        }
    }
}
