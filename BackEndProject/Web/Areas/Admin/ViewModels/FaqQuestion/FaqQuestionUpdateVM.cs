using Microsoft.AspNetCore.Mvc.Rendering;

namespace Web.Areas.Admin.ViewModels.FaqQuestion
{
    public class FaqQuestionUpdateVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public int FaqCategoryId { get; set; }
        public List<SelectListItem>? FaqCategory { get; set; }
    }
}
