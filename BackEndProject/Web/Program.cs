using Core.Entities;
using Core.Utilities.FileService;
using DataAccess;
using DataAccess.Contexts;
using DataAccess.Repositories.Abstract;
using DataAccess.Repositories.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Web.Services.Abstract;
using Web.Services.Concrete;
using AdminAbstractService = Web.Areas.Admin.Services.Abstract;
using AdminConcreteService = Web.Areas.Admin.Services.Concrete;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

var connectionString = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<AppDbContext>(x => x.UseSqlServer(connectionString, x => x.MigrationsAssembly("DataAccess")));

builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredUniqueChars = 0;
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 0;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;

    options.User.RequireUniqueEmail = true;

})
    .AddEntityFrameworkStores<AppDbContext>();

#region Repositories

builder.Services.AddScoped<IHomeMainSliderRepository, HomeMainSliderRepository>();
builder.Services.AddScoped<IOurVisionRepository, OurVisionRepository>();
builder.Services.AddScoped<IMedicalDepartamentRepository, MedicalDepartamentRepository>();
builder.Services.AddScoped<IAboutRepository, AboutRepository>();
builder.Services.AddScoped<IAboutPhotoRepository, AboutPhotoRepository>();
builder.Services.AddScoped<IHomeVideoComponentRepository, HomeVideoComponentRepository>();
builder.Services.AddScoped<IHomeChooseComponentRepository, HomeChooseComponentRepository>();
builder.Services.AddScoped<ILastestNewRepository, LastestNewRepository>();
builder.Services.AddScoped<IPriceRepository, PriceRepository>();
builder.Services.AddScoped<IFaqCategoryRepository, FaqCategoryRepository>();
builder.Services.AddScoped<IFaqQuestionRepository, FaqQuestionRepository>();
builder.Services.AddScoped<IProductCategoryRepository, ProductCategoryRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IDoctorRepository, DoctorRepository>();
builder.Services.AddScoped<IBasketRepository, BasketRepository>();
builder.Services.AddScoped<IBasketProductRepository, BasketProductRepository>();
builder.Services.AddScoped<IStatisticRepository, StatisticRepository>();

#endregion

#region Services

builder.Services.AddScoped<IHomeService, HomeService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IMedicalDepartamentService, MedicalDepartamentService>();
builder.Services.AddScoped<IPriciningsService, PricingsService>();
builder.Services.AddScoped<IFaqService, FaqService>();
builder.Services.AddScoped<IShopService, ShopService>();
builder.Services.AddScoped<IDoctorService, DoctorService>();
builder.Services.AddScoped<IBasketService, BasketService>();
builder.Services.AddScoped<AdminAbstractService.IAboutService, AdminConcreteService.AboutService>();
builder.Services.AddScoped<AdminAbstractService.IOurVisionService, AdminConcreteService.OurVisionService>();
builder.Services.AddScoped<AdminAbstractService.IHomeMainSliderService, AdminConcreteService.HomeMainSliderService>();
builder.Services.AddScoped<AdminAbstractService.IAccountService, AdminConcreteService.AccountService>();
builder.Services.AddScoped<AdminAbstractService.IMedicalDepartamentService, AdminConcreteService.MedicalDepartamentService>();
builder.Services.AddScoped<AdminAbstractService.IHomeVideoComponentService, AdminConcreteService.HomeVideoComponentService>();
builder.Services.AddScoped<AdminAbstractService.IHomeChooseComponentService, AdminConcreteService.HomeChooseComponentService>();
builder.Services.AddScoped<AdminAbstractService.ILastestNewService, AdminConcreteService.LastestNewService>();
builder.Services.AddScoped<AdminAbstractService.IPriceService, AdminConcreteService.PriceService>();
builder.Services.AddScoped<AdminAbstractService.IFaqCategoryService, AdminConcreteService.FaqCategoryService>();
builder.Services.AddScoped<AdminAbstractService.IFaqQuestionService, AdminConcreteService.FaqQuestionService>();
builder.Services.AddScoped<AdminAbstractService.IProductCategoryService, AdminConcreteService.ProductCategoryService>();
builder.Services.AddScoped<AdminAbstractService.IProductService, AdminConcreteService.ProductService>();
builder.Services.AddScoped<AdminAbstractService.IDoctorService, AdminConcreteService.DoctorService>();
builder.Services.AddScoped<AdminAbstractService.IStatisticService, AdminConcreteService.StatisticService>();



#endregion

#region UtilitiesServices

builder.Services.AddSingleton<IFileService, FileService>();
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=account}/{action=login}/{id?}"
    );

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


var scopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();

using (var scope = scopeFactory.CreateScope())
{
    var userManager = scope.ServiceProvider.GetService<UserManager<User>>();
    var roleManager = scope.ServiceProvider.GetService<RoleManager<IdentityRole>>();
    await DbInitializer.SeedAsync(userManager, roleManager);
}


app.Run();