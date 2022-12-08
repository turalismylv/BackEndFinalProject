using Core.Constans;
using System.ComponentModel.DataAnnotations;

namespace Web.Areas.Admin.ViewModels.Doctor
{
    public class DoctorCreateVM
    {
        public string FullName { get; set; }
        public IFormFile MainPhoto { get; set; }
        public DoctorSpecialtys Specialty { get; set; }
        public string Qualification { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required,DataType(DataType.EmailAddress)]
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
