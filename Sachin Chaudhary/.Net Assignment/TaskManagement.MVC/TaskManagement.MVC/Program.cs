using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System;
using TaskManagement.MVC.Filters;
using TaskManagement.MVC.Handlers;
using TaskManagement.MVC.Helpers;
using TaskManagement.MVC.Interfaces;
using TaskManagement.MVC.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews(options =>
{
   
    options.Filters.Add<ValidationFilter>();

    options.Filters.Add<LoggingFilter>();

    options.Filters.Add<GlobalExceptionFilter>();
});

builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ITaskService, TaskService>();


builder.Services.AddTransient<TokenRefreshHandler>();
builder.Services.AddTransient<AuthenticatingHandler>();

builder.Services.AddHttpClient<IApiService, ApiService>(client =>
{
  
    client.BaseAddress = new Uri("https://localhost:7244/");
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});

builder.Services.AddHttpClient<IApiService, ApiService>()
    .AddHttpMessageHandler<TokenRefreshHandler>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";       
        options.LogoutPath = "/Account/Logout";
        options.AccessDeniedPath = "/Account/AccessDenied";
        options.Cookie.Name = "TaskManagementAuthCookie";
        options.Cookie.HttpOnly = true;      
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
        options.SlidingExpiration = true;
    });
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();


app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();