using System.ComponentModel.DataAnnotations;

namespace Web.Areas.Admin.ViewModels.LastestNew
{
    public class LastestNewCreateVM
    {
       
        public string Title { get; set; }

        public DateTime Time { get; set; }

        public IFormFile MainPhoto { get; set; }
        public string Text { get; set; }

    }
}
