using Microsoft.EntityFrameworkCore;
using WebSiteLKDT.Models;
using WebSiteLKDT.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.CodeAnalysis;
using System.Net.NetworkInformation;
using X.PagedList;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var connectionString=builder.Configuration.GetConnectionString("WslkdtContext");
builder.Services.AddDbContext<WslkdtContext>(x=>x.UseSqlServer(connectionString));
builder.Services.AddScoped<LoaiSPRepository, ILoaiSPRepository>();
builder.Services.AddSession();

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
app.UseSession();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=TrangChu}/{id?}");

app.Run();
