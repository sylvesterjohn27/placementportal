using PlacementManagement.DAL;
using PlacementManagement.DAL.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using PlacementManagement.BAL.Services.Interfaces;
using PlacementManagement.DAL.Repository.Implementations;
using PlacementManagement.BAL.Services.Implementations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;

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
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IUserServices, UserServices>();
builder.Services.AddTransient<IMasterRepository, MasterRepository>();
builder.Services.AddTransient<IMasterServices, MasterServices>();
builder.Services.AddTransient<IStudentRepository, StudentRepository>();
builder.Services.AddTransient<IStudentServices, StudentServices>();
builder.Services.AddTransient<IInterviewProcessRepository, InterviewProcessRepository>();
builder.Services.AddTransient<IInterviewProcessServices, InterviewProcessServices>();
//DI
builder.Services.AddDbContext<PlacementManagementAppDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);


builder.Services.AddIdentity<IdentityUser, IdentityRole>()           
            .AddEntityFrameworkStores<PlacementManagementAppDbContext>()
            .AddDefaultTokenProviders();

builder.Services.PostConfigure<CookieAuthenticationOptions>(IdentityConstants.ApplicationScheme,
options =>
{
    options.LoginPath = "/Account/Login";
    options.LogoutPath = "/Account/Logout";
    options.AccessDeniedPath = "/Account/AccessDenied";
    options.SlidingExpiration = true;
});

builder.Services.AddAuthorization(options =>
{   
    options.AddPolicy("rolecreation", policy =>
    policy.RequireRole("Admin")
    );
});

builder.Services.AddMvc();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();
app.Run();