using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Abstract
{
    public interface IFaqQuestionRepository : IRepository<FaqQuestion>
    {
        Task<List<FaqQuestion>> GetAllGetCategoryAsync();
        Task<List<FaqQuestion>> GetByCategoryIdAsync(int categoryId);
    }
}
