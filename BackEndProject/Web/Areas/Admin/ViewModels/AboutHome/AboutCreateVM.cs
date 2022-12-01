namespace Web.Areas.Admin.ViewModels.AboutHome
{
    public class AboutCreateVM
    {
        public string Title { get; set; }
      
        public string Description { get; set; }
     
        public IFormFile MainPhoto { get; set; }

        public List<IFormFile>? Photos { get; set; }
    }
}
