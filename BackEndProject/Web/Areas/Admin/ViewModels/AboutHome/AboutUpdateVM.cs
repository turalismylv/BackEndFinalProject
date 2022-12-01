namespace Web.Areas.Admin.ViewModels.AboutHome
{
    public class AboutUpdateVM
    {

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile? MainPhoto { get; set; }
        public string? MainPhotoPath { get; set; }
        public List<IFormFile>? Photos { get; set; }

        public ICollection<Core.Entities.AboutPhoto>? aboutHomePhotos { get; set; }
    }
}
