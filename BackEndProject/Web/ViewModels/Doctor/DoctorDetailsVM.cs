using Core.Constans;
using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels.Doctor
{
    public class DoctorDetailsVM
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string MainPhoto { get; set; }
        public DoctorSpecialtys Specialty { get; set; }
        public string Qualification { get; set; }
        public string PhoneNumber { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string WorkingTime { get; set; }
        public string Skill { get; set; }
        public string Description { get; set; }
        public bool HomePageSee { get; set; }
        public string FbUrl { get; set; }
        public string TwUrl { get; set; }
        public string LiUrl { get; set; }
    }
}
