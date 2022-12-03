namespace Web.ViewModels.Doctor
{
    public class DoctorIndexVM
    {
        public List<Core.Entities.Doctor> Doctors { get; set; }

        public int Page { get; set; } = 1;

        public int Take { get; set; } = 2;

        public int PageCount { get; set; }
        public string? FullName { get; set; }


    }
}
