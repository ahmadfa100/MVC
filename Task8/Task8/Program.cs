using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Task8.Data;
using Task8.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<UserFromIdentity>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Ensure the roles used by the controllers exist before anyone registers.
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    foreach (var roleName in new[] { "User", "Admin", "SuperAdmin" })
    {
        if (!await roleManager.RoleExistsAsync(roleName))
        {
            var result = await roleManager.CreateAsync(new IdentityRole(roleName));
            if (!result.Succeeded)
                throw new InvalidOperationException($"Could not create the {roleName} role: {string.Join(", ", result.Errors.Select(error => error.Description))}");
        }
    }

    // A registration attempted before the User role existed may have created an account without a role.
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<UserFromIdentity>>();
    var users = await userManager.Users.ToListAsync();
    foreach (var user in users)
    {
        if ((await userManager.GetRolesAsync(user)).Count == 0)
        {
            var result = await userManager.AddToRoleAsync(user, "User");
            if (!result.Succeeded)
                throw new InvalidOperationException($"Could not assign the User role to {user.Email}: {string.Join(", ", result.Errors.Select(error => error.Description))}");
        }
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapRazorPages()
   .WithStaticAssets();

app.Run();
