using Practica_Net_Core.Recursos;
using Practica_Net_Core.Repositorios.Clases;
using Practica_Net_Core.Repositorios.Interfaces;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddRazorPages(); 
builder.Services.AddMvc(); // Añadir servicios de MVC

// Inyección de dependencias para SqlServer y repositorios
builder.Services.AddSingleton<SqlServer>(); 
builder.Services.AddTransient<IProductoRepositorio, ProductoRepositorio>(); 
builder.Services.AddTransient<ITipoProductoRepositorio, TipoProductoRepositorio>(); 
builder.Services.AddTransient<IProveedorRepositorio, ProveedorRepositorio>(); 
builder.Services.AddTransient<IProductoProveedorRepositorio, ProveedorProductoRepositorio>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages(); // Mapear páginas de Razor

app.Run();
