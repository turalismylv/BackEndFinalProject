namespace Web.Areas.Admin.ViewModels.LastestNew
{
    public class LastestNewUpdateVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime Time { get; set; }
        public string? MainPhotoName { get; set; }
        public IFormFile? MainPhoto { get; set; }
        public string Text { get; set; }
    }
}
