namespace Web.Areas.Admin.ViewModels.AboutHome
{
    public class AboutDetailsVM
    {

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string? MainPhotoPath { get; set; }

        public ICollection<Core.Entities.AboutPhoto>? aboutHomePhotos { get; set; }
    }
}
