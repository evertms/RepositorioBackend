using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using ProyectoFinal.Context;
using ProyectoFinal.Models;
using ProyectoFinal.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy =>
        {
            policy.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});
builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IProyectoService, ProyectoService>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IProyectoAprobacionService, ProyectoAprobacionService>();
builder.Services.AddScoped<IProyectoFiltroService, ProyectoFiltroService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<INotificacionService, NotificacionService>();
builder.Services.AddScoped<IAreasConocimientoService, AreasConocimientoService>();
builder.Services.AddScoped<ITiposTrabajoService, TiposTrabajoService>();
//builder.Services.AddHostedService<RecordatorioRevisoresService>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseCors("AllowFrontend");

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(@"C:\Users\evert\Documents\GitHub\Proyecto-Final-DSW\Repositorio\uploads"),
    RequestPath = "/uploads"
});

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(@"C:\Users\evert\RiderProjects\ProyectoFinal\ProyectoFinal\uploads"),
    RequestPath = "/uploads"
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();