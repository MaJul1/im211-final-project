using Microsoft.EntityFrameworkCore;
using PLSPEduView.Context;
using PLSPEduView.Repository;
using PLSPEduView.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<SystemDatabaseContext>(options =>
    options.UseMySql
    (
        builder.Configuration.GetConnectionString("MySqlConnectionString"), 
        new MySqlServerVersion(ServerVersion.Parse(builder.Configuration.GetConnectionString("MySqlVersion")))
    )
);

builder.Services.AddScoped<CourseRepository>();
builder.Services.AddScoped<StudentRepository>();
builder.Services.AddScoped<SkillRepository>();
builder.Services.AddScoped<HomeViewService>();
builder.Services.AddScoped<SeederService>();
builder.Services.AddScoped<StudentViewService>();
builder.Services.AddScoped<ConfigurationService>();
builder.Services.AddScoped<ProgramRepository>();
builder.Services.AddScoped<DepartmentRepository>();
builder.Services.AddScoped<CourseViewService>();
builder.Services.AddScoped<SkillViewService>();
builder.Services.AddScoped<StudentWriteModelService>();
builder.Services.AddScoped<SelectListService>();
builder.Services.AddScoped<CourseWriteModelService>();
builder.Services.AddScoped<SkillWriteModelService>();

builder.Services.AddControllersWithViews();
builder.Services.AddSession();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider.GetService<SeederService>();
    await seeder!.SeedDataAsync();
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

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
