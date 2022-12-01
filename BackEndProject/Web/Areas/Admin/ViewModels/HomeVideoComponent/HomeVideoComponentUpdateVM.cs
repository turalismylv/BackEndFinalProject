namespace Web.Areas.Admin.ViewModels.HomeVideoComponent
{
    public class HomeVideoComponentUpdateVM
    {
        public int Id { get; set; }
        public IFormFile? CoverPhoto { get; set; }
        public string? MainPhotoPath { get; set; }

        public string VideoUrl { get; set; }

    }
}
