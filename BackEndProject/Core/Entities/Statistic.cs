﻿using Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Statistic :BaseEntity
    {

        public string Title { get; set; }
        public int Quantity { get; set; }
    }
}
