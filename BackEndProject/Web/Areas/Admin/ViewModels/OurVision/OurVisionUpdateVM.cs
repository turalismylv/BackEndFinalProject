namespace Web.Areas.Admin.ViewModels.OurVision
{
    public class OurVisionUpdateVM
    {

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string? MainPhotoName { get; set; }
        public IFormFile? MainPhoto { get; set; }
    }
}
