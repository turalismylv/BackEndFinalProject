namespace Web.Areas.Admin.ViewModels.HomeChooseComponent
{
    public class HomeChooseComponentCreateVM
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Text { get; set; }
       
        public IFormFile MainPhoto { get; set; }
    }
}
