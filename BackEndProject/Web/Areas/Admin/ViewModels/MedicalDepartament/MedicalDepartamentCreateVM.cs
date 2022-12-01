namespace Web.Areas.Admin.ViewModels.MedicalDepartament
{
    public class MedicalDepartamentCreateVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile MainPhoto { get; set; }
    }
}
