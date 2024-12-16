using Microsoft.EntityFrameworkCore;
using ProyectoFinal.Context;
using ProyectoFinal.Models;
using ProyectoFinal.Models.DTO;
using ProyectoFinal.Services.Contrato;

namespace ProyectoFinal.Services.Implementacion;

public class NotificacionService : INotificacionService
{
    private readonly AppDbContext _context;
    private readonly IEmailService _emailService;

    public NotificacionService(AppDbContext context, IEmailService emailService)
    {
        _context = context;
        _emailService = emailService;
    }

    public void CrearNotificacion(NotificacionDTO notificacionDTO)
    {
        var notificacion = new Notificacion
        {
            Idusuario = notificacionDTO.IDUsuario,
            RolReceptor = notificacionDTO.RolReceptor,
            Mensaje = notificacionDTO.Mensaje,
            FechaCreacion = DateTime.Now,
            Leida = false
        };

        _context.Notificaciones.Add(notificacion);
        _context.SaveChanges();
    }

    public void NotificarRevisorSobreProyecto(Proyecto proyecto)
    {
        // Obtén el email del revisor
        var revisor = _context.Usuarios.FirstOrDefault(u => u.Rol == "Revisor");
        if (revisor == null) return;

        var mensaje = $"Un nuevo proyecto titulado '{proyecto.Titulo}' ha sido subido por el docente {proyecto.Idusuario}. Por favor, revísalo.";

        // Crear notificación interna
        CrearNotificacion(new NotificacionDTO
        {
            IDUsuario = revisor.Idusuario,
            RolReceptor = "Revisor",
            Mensaje = mensaje
        });

        // Enviar correo al revisor
        _emailService.EnviarEmail(revisor.Correo, "Nuevo Proyecto para Revisar", mensaje);
    }

    public void NotificarDocenteSobreEstado(Proyecto proyecto, string estado, string comentarios)
    {
        var docente = _context.Usuarios.Find(proyecto.Idusuario);
        if (docente == null) return;

        var mensaje = $"El proyecto '{proyecto.Titulo}' ha sido {estado}. Comentarios del revisor: {comentarios}.";

        // Crear notificación interna
        CrearNotificacion(new NotificacionDTO
        {
            IDUsuario = docente.Idusuario,
            RolReceptor = "Docente",
            Mensaje = mensaje
        });

        // Enviar correo al docente
        _emailService.EnviarEmail(docente.Correo, "Estado de tu Proyecto", mensaje);
    }

    public void EnviarRecordatoriosARevisores()
    {
        var proyectosPendientes = _context.Proyectos
            .Where(p => p.EstatusAprobacion == "Pendiente" && 
                        p.FechaSubida.HasValue && 
                        EF.Functions.DateDiffDay(p.FechaSubida.Value, DateTime.Now) >= 1)
            .ToList();

        var revisor = _context.Usuarios.FirstOrDefault(u => u.Rol == "Revisor");
        if (revisor == null) return;

        foreach (var proyecto in proyectosPendientes)
        {
            var mensaje = $"El proyecto '{proyecto.Titulo}' lleva más de 7 días pendiente de revisión.";
            _emailService.EnviarEmail(revisor.Correo, "Recordatorio de Proyecto Pendiente", mensaje);
        }
    }
}