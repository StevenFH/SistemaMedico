using Microsoft.EntityFrameworkCore;
using SistemaMedico.Infrastructure.Data;
using SistemaMedico.Domain.Interfaces;
using SistemaMedico.Infrastructure.Repositories;
using SistemaMedico.Application.Interfaces;
using SistemaMedico.Application.Services;
using SistemaMedico.Application.Mappings;
using SistemaMedico.Middleware;
using System.Globalization;
using Microsoft.AspNetCore.Localization;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// 1. Inyección del DbContext apuntando a Infrastructure para las migraciones
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString,
    b => b.MigrationsAssembly("SistemaMedico.Infrastructure")));

// 2. Inyección del Repositorio Genérico
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

// 3. Inyección de Servicios (Capa Application)
builder.Services.AddScoped<ICategoriaService, CategoriaService>();
builder.Services.AddScoped<IProductoService, ProductoService>();

// 4. Configuración de AutoMapper
builder.Services.AddAutoMapper(cfg => { }, typeof(MappingProfile));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//Inyección de tu middleware global
app.UseMiddleware<MiddlewareGlobalExceptionHandler>();

var supportedCultures = new[] { new CultureInfo("es-CR") };
app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("es-CR"),
    SupportedCultures = supportedCultures,
    SupportedUICultures = supportedCultures
});

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
