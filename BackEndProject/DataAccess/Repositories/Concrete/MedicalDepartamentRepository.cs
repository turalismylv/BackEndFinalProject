﻿using Core.Entities;
using DataAccess.Contexts;
using DataAccess.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Concrete
{
    public class MedicalDepartamentRepository : Repository<MedicalDepartament>, IMedicalDepartamentRepository
    {
        public MedicalDepartamentRepository(AppDbContext context) : base(context) { }

    }
   
}
