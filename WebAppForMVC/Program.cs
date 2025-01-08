using Microsoft.EntityFrameworkCore;
using WebAppForMVC.Context;
using WebAppForMVC.Repository;
using WebAppForMVC.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<SystemDatabaseContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("SqlLiteConnectionString"))
);

builder.Services.AddScoped<CourseRepository>();
builder.Services.AddScoped<StudentRepository>();
builder.Services.AddScoped<SkillRepository>();
builder.Services.AddScoped<HomeViewService>();
builder.Services.AddScoped<SeederService>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider.GetService<SeederService>();
    await seeder!.SeedData();
}

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
