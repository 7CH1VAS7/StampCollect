using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;
using System.Text.Json.Serialization;
using Курсовая.Domain;
using Курсовая.Domain.Repository.Abstract;
using Курсовая.Domain.Repository.Concrete;
using Курсовая.Models;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection")));
builder.Services.AddScoped<IStampRepository, EFStampRepository>();
builder.Services.AddScoped<ICollectorRepository, EFCollectorRepository>();
builder.Services.AddScoped<ICollectionRepository, EFCollectionRepository>();
builder.Services.AddScoped<DataManager>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.Name = "TestCookie"; // Имя cookie
        options.LoginPath = "/Account/Index";  // Путь к странице входа
        options.AccessDeniedPath = "/Account/Error"; // Путь при отказе в доступе
        options.ExpireTimeSpan = TimeSpan.FromDays(30); // Время жизни cookie
        options.SlidingExpiration = true; // Обновлять срок при активности
    });

// Добавляем сервисы авторизации
builder.Services.AddAuthorization();



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

app.UseAuthentication(); // Должно быть ДО UseAuthorization
app.UseAuthorization();

app.MapControllerRoute( 
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();
