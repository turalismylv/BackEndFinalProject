using Core.Entities;
using DataAccess.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.Statistic;

namespace Web.Areas.Admin.Services.Concrete
{
    public class StatisticService : IStatisticService
    {
        private readonly ModelStateDictionary _modelState;
        private readonly IStatisticRepository _statisticRepository;

        public StatisticService(IStatisticRepository statisticRepository, IActionContextAccessor actionContextAccessor)
        {
            _modelState = actionContextAccessor.ActionContext.ModelState;
            _statisticRepository = statisticRepository;
        }

        public async Task<StatisticIndexVM> GetAllAsync()
        {
            var model = new StatisticIndexVM
            {
                Statistics = await _statisticRepository.GetAllAsync()
            };
            return model;
        }


        public async Task<bool> CreateAsync(StatisticCreateVM model)
        {
            if (!_modelState.IsValid) return false;

            var isExist = await _statisticRepository.AnyAsync(c => c.Title.Trim().ToLower() == model.Title.Trim().ToLower());
            if (isExist)
            {
                _modelState.AddModelError("Title", "Bu adda statika mövcuddur");
                return false;
            }

         



            var statistic = new Statistic
            {

                Title = model.Title,
                CreatedAt = DateTime.Now,
                Quantity = model.Quantity,

            };

            await _statisticRepository.CreateAsync(statistic);
            return true;
        }


        public async Task<StatisticUpdateVM> GetUpdateModelAsync(int id)
        {


            var statistic = await _statisticRepository.GetAsync(id);

            if (statistic == null) return null;

            var model = new StatisticUpdateVM
            {
                Id = statistic.Id,
                Title = statistic.Title,
                Quantity = statistic.Quantity,
            };

            return model;

        }

        public async Task<bool> UpdateAsync(StatisticUpdateVM model)
        {
            if (!_modelState.IsValid) return false;

            var isExist = await _statisticRepository.AnyAsync(c => c.Title.Trim().ToLower() == model.Title.Trim().ToLower() && c.Id != model.Id);
            if (isExist)
            {
                _modelState.AddModelError("Title", "Bu adda statistik mövcuddur");
                return false;
            }
           

            var statistic = await _statisticRepository.GetAsync(model.Id);




            if (statistic != null)
            {
                statistic.Id = model.Id;
                statistic.Title = model.Title;
                statistic.ModifiedAt = DateTime.Now;
                statistic.Quantity = model.Quantity;


             

                await _statisticRepository.UpdateAsync(statistic);

            }
            return true;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var statistic = await _statisticRepository.GetAsync(id);
            if (statistic != null)
            {
                await _statisticRepository.DeleteAsync(statistic);
                return true;

            }
            return false;
        }
    }
}
