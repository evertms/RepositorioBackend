using ProyectoFinal.Context;
using ProyectoFinal.Models;

namespace ProyectoFinal.Services;

public class ProyectoService : IProyectoService
{
    private readonly AppDbContext _context;
    
    public ProyectoService(AppDbContext context, INotificacionService notificacionService, IEmailService emailService)
    {
        _context = context;
    }

    public IEnumerable<Proyecto> ObtenerTodos()
    {
        return _context.Proyectos.ToList();
    }

    public Proyecto ObtenerPorId(int id)
    {
        return _context.Proyectos.FirstOrDefault(p => p.Idproyecto == id);
    }

    public void CrearProyecto(Proyecto proyecto)
    {
        proyecto.FechaSubida = DateTime.Now; // Asignar fecha actual
        proyecto.EstatusAprobacion = "Pendiente"; // Por defecto
        _context.Proyectos.Add(proyecto);
        _context.SaveChanges();
    }

    public void ActualizarProyecto(Proyecto proyecto)
    {
        var existente = _context.Proyectos.Find(proyecto.Idproyecto);
        if (existente == null) throw new Exception("Proyecto no encontrado");

        existente.Titulo = proyecto.Titulo;
        existente.Resumen = proyecto.Resumen;
        existente.EnlaceRepositorio = proyecto.EnlaceRepositorio;
        existente.DocumentoPdf = proyecto.DocumentoPdf;
        existente.Estado = proyecto.Estado;
        existente.EstatusAprobacion = proyecto.EstatusAprobacion;

        _context.SaveChanges();
    }

    public void EliminarProyecto(int id)
    {
        var proyecto = _context.Proyectos.Find(id);
        if (proyecto == null) throw new Exception("Proyecto no encontrado");

        _context.Proyectos.Remove(proyecto);
        _context.SaveChanges();
    }
}