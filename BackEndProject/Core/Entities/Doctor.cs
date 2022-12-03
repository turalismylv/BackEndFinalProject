using Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Doctor :BaseEntity
    {
        public string FullName { get; set; }

        public string MainPhoto { get; set; }
        public string Specialty { get; set; }
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
