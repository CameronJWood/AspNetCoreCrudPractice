using AspNetCoreCrudPractice.Data;
using Microsoft.EntityFrameworkCore;
using Serilog;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//inject dbcontext into our services
builder.Services.AddDbContext<MVCDemoDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("MvcDemoConnectionString")));

builder.Host.UseSerilog((hostContext, services, configuration) => {
    configuration
        .MinimumLevel.Debug()
        .WriteTo.Console()
        .WriteTo.File($"E:\\Codeflux\\Logs\\Practice AspCore MVC Sameer\\log-{DateTime.Now:yyyyMMddHHmmss}.txt");
});


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

app.MapControllerRoute( //this is middleware?
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
