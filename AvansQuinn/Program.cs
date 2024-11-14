using System.Diagnostics;
using Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Persistence;

var builder = WebApplication.CreateBuilder(args);


var primaryConnectionString = builder.Configuration.GetConnectionString("PrimaryDatabase");
Debug.Assert(primaryConnectionString != null, nameof(primaryConnectionString) + " != null");
Console.WriteLine("Primary DB Connection String in Program.cs:");
Console.WriteLine(primaryConnectionString);
builder.Services.AddDbContext<SqlServerDb>(options => 
    options.UseSqlServer(primaryConnectionString));


// Secondary Database Connection (Identity Data)
var secondaryConnectionString = builder.Configuration.GetConnectionString("SecondaryDatabase");
Debug.Assert(secondaryConnectionString != null, "SecondaryDatabase connection string is missing");
Console.WriteLine("Identity DB Connection String in Program.cs:");
Console.WriteLine(secondaryConnectionString);

builder.Services.AddDbContext<SqlServerIdentityDb>(options =>
    options.UseSqlServer(secondaryConnectionString));

// Identity and Authentication Configuration
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<SqlServerIdentityDb>()
    .AddDefaultTokenProviders();

// Global Authorization Policy
builder.Services.AddMvc(options =>
{
    var policy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
    options.Filters.Add(new AuthorizeFilter(policy));
}).AddXmlSerializerFormatters();

// Add controllers with views
builder.Services.AddControllersWithViews();

// Register custom repositories
builder.Services.AddScoped<IMealBoxRepository, MealBoxRepository>();
builder.Services.AddScoped<ICanteenRepository, CanteenRepository>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IMealboxProductRepository, MealBoxProductRepository>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();

var app = builder.Build();

// Configure HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

app.Run();
