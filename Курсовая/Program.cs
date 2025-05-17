using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Курсовая.Domain;
using Курсовая.Domain.Repository.Abstract;
using Курсовая.Domain.Repository.Concrete;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection")));
builder.Services.AddScoped<IStampRepository, EFStampRepository>();
builder.Services.AddScoped<ICollectorRepository, EFCollectorRepository>();
builder.Services.AddScoped<ICollectionRepository, EFCollectionRepository>();
builder.Services.AddScoped<DataManager>();


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

app.UseAuthorization();

app.MapControllerRoute( 
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
