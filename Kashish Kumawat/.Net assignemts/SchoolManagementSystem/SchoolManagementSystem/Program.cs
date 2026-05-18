using Microsoft.EntityFrameworkCore;
using SchoolManagementSystem.Models;

var builder = WebApplication.CreateBuilder(args);

var connectionString = "Server=DESKTOP-OFI0SIG;Database=school_erp;Trusted_Connection=True;TrustServerCertificate=True";



builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<SchoolErpContext>(options =>
    options.UseSqlServer(connectionString));

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.MapStaticAssets();

app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();