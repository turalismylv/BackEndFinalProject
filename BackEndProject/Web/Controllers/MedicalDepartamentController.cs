using Microsoft.AspNetCore.Mvc;
using Web.Services.Abstract;

namespace Web.Controllers
{
    public class MedicalDepartamentController : Controller
    {

        private readonly IMedicalDepartamentService _medicalDepartamentService;

        public MedicalDepartamentController(IMedicalDepartamentService medicalDepartamentService)
        {
            _medicalDepartamentService = medicalDepartamentService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = await _medicalDepartamentService.GetAllAsync();
            return View(model);
        }
    }
}
