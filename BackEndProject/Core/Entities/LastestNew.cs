﻿using Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class LastestNew :BaseEntity
    {
        
        public string Title { get; set; }
        public DateTime Time { get; set; }

        public string Text { get; set; }

        public string PhotoName { get; set; }
    }
}
