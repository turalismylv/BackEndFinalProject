using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.MedicalDepartament;

namespace Web.Areas.Admin.Controllers
{

    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class MedicalDepartamentController : Controller
    {

        private readonly IMedicalDepartamentService _medicalDepartamentService;

        public MedicalDepartamentController(IMedicalDepartamentService medicalDepartamentService)
        {

            _medicalDepartamentService = medicalDepartamentService;
        }
        #region MedicalDepartament


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = await _medicalDepartamentService.GetAllAsync();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Create(MedicalDepartamentCreateVM model)
        {
            var isSucceeded = await _medicalDepartamentService.CreateAsync(model);
            if (isSucceeded) return RedirectToAction(nameof(Index));
            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var model = await _medicalDepartamentService.GetUpdateModelAsync(id);
            if (model == null) return NotFound();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, MedicalDepartamentUpdateVM model)
        {
            if (id != model.Id) return NotFound();

            var isSucceeded = await _medicalDepartamentService.UpdateAsync(model);
            if (isSucceeded) return RedirectToAction(nameof(Index));

            model = await _medicalDepartamentService.GetUpdateModelAsync(id);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var isSucceded = await _medicalDepartamentService.DeleteAsync(id);
            if (isSucceded)
            {
                return RedirectToAction(nameof(Index));
            }
            return NotFound();

            #endregion
        }
    }
}
