﻿using Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class FaqQuestion:BaseEntity
    {

        public string Title { get; set; }
        public string Description { get; set; }

        public int FaqCategoryId { get; set; }

        public FaqCategory FaqCategory { get; set; }
    }
}
