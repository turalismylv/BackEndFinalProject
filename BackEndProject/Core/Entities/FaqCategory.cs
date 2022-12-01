using Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class FaqCategory :BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public ICollection<FaqQuestion> FaqQuestions { get; set; }
    }
}
