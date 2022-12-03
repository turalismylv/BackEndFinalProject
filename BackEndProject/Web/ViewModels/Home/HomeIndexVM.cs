namespace Web.ViewModels.Home
{
    public class HomeIndexVM
    {
        public List<Core.Entities.HomeMainSlider> HomeMainSliders { get; set; }
        public List<Core.Entities.OurVision> ourVisions { get; set; }

        public List<Core.Entities.MedicalDepartament> medicalDepartaments { get; set; }
        public List<Core.Entities.LastestNew> LastestNews { get; set; }
        public List<Core.Entities.Doctor> Doctors { get; set; }
        public Core.Entities.About About { get; set; }
        public Core.Entities.HomeVideoComponent homeVideoComponent { get; set; }
        public Core.Entities.HomeChooseComponent HomeChooseComponent { get; set; }


    }
}
