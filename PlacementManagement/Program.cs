using PlacementManagement.DAL;
using PlacementManagement.DAL.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using PlacementManagement.BAL.Services.Interfaces;
using PlacementManagement.DAL.Repository.Implementations;
using PlacementManagement.BAL.Services.Implementations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//Repo
builder.Services.AddTransient<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddTransient<IPlacementRequestRepository, PlacementRequestRepository>();
builder.Services.AddTransient<IMasterRepository, MasterRepository>();

//Service
builder.Services.AddTransient<IEmployeeServices, EmployeeServices>();
builder.Services.AddTransient<IPlacementRequestServices, PlacementRequestServices>();
builder.Services.AddTransient<IMasterServices, MasterServices>();

//DI
builder.Services.AddDbContext<PlacementManagementAppDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
