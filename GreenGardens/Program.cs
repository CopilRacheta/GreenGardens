using Microsoft.EntityFrameworkCore;
using GreenGardens.Model;
using Microsoft.AspNetCore.Authentication.Cookies;
using GreenGardens.Data;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Register the DbContext for the application, specifying to use SQL Server with the connection string obtained earlier.
// AppDbContext is the class managing the database context.
builder.Services.AddDbContext<AppDbContext>(option => option.UseSqlServer(connectionString));
//Register a singleton instance of product
builder.Services.AddSingleton<CustomersModel>();
builder.Services.AddSingleton<ProductModel>();
builder.Services.AddSingleton<OrderModel>();
// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDistributedMemoryCache();  // Necessary for session state
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Set session timeout
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});


builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(option => option.UseSqlServer(connectionString));

builder.Services.AddAuthentication("Cookies")
    .AddCookie("Cookies", options =>
     {
         options.LoginPath = "/Login";
         options.LogoutPath = "/Logout";
     });

// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.MapRazorPages();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.UseSession();

app.Run();
