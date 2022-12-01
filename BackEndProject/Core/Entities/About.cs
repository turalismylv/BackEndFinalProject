using Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class About : BaseEntity
    {
        
        public string Title { get; set; }

        public string Description { get; set; }

        public string MainPhoto { get; set; }

        public ICollection<AboutPhoto> AboutPhotos { get; set; }
    }
}
