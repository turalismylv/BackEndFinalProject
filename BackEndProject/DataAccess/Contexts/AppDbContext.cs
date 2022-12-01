using Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Contexts
{
    public class AppDbContext : IdentityDbContext<User>
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<HomeMainSlider> HomeMainSliders { get; set; }
        public DbSet<OurVision> OurVisions { get; set; }
        public DbSet<MedicalDepartament> MedicalDepartaments { get; set; }
        public DbSet<About> About { get; set; }
        public DbSet<AboutPhoto> AboutPhotos { get; set; }
        public DbSet<HomeVideoComponent> HomeVideoComponent { get; set; }
        public DbSet<HomeChooseComponent> HomeChooseComponent { get; set; }
        public DbSet<LastestNew> LastestNews { get; set; }
        public DbSet<Price> Prices { get; set; }
        public DbSet<FaqQuestion> FaqQuestions { get; set; }
        public DbSet<FaqCategory> FaqCategories { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Product> Products { get; set; }

    }
}
