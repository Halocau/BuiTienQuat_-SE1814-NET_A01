using BuiTienQuatMVC.Models;
using BuiTienQuatMVC.Repositories;
using BuiTienQuatMVC.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Thêm Authentication và Authorization
builder.Services.AddAuthentication("Cookies")
    .AddCookie("Cookies", options =>
    {
        options.LoginPath = "/SystemAccount/Login";
        options.AccessDeniedPath = "/SystemAccount/AccessDenied";
    });
builder.Services.AddAuthorization();
builder.Services.AddSession();
//add services for database
builder.Services.AddDbContext<FunewsManagementContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
    );

// Đăng ký repository và service vào DI container
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<CategoryService>();

builder.Services.AddScoped<INewsArticleRepository, NewsArticleRepository>();
builder.Services.AddScoped<NewsArticleServicecs>();

//account
builder.Services.AddScoped<ISystemAccountRepository, SystemAccountRepository>();
builder.Services.AddScoped<SystemAccountService>();

//Cấu hình Session
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Thời gian timeout của session
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddHttpContextAccessor(); // Đảm bảo có HttpContext để truy cập session

var app = builder.Build();
app.UseSession();
app.UseAuthorization();
app.UseAuthentication();
// Khởi tạo dữ liệu admin
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<FunewsManagementContext>();
    var config = scope.ServiceProvider.GetRequiredService<IConfiguration>();
    AdminAccount.SeedAdminAccount(context, config);
}


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
    pattern: "{controller=NewsArticle}/{action=Index}/{id?}");

app.Run();
