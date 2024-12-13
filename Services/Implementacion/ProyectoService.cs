using ProyectoFinal.Context;
using ProyectoFinal.Models;
using ProyectoFinal.Models.DTO;

namespace ProyectoFinal.Services;

public class ProyectoService : IProyectoService
{
    private readonly AppDbContext _context;

    public ProyectoService(AppDbContext context, INotificacionService notificacionService, IEmailService emailService)
    {
        _context = context;
    }

    public IEnumerable<Proyecto> ObtenerTodosAprobados()
    {
        return _context.Proyectos.Where(p => p.EstatusAprobacion.ToLower() == "aprobado").ToList();
    }

    public Proyecto ObtenerPorId(int id)
    {
        return _context.Proyectos.FirstOrDefault(p => p.Idproyecto == id);
    }

    public Proyecto CrearProyectoConArchivo(ProyectoCrearDTO proyectoDTO, IFormFile archivo)
    {
        if (proyectoDTO == null)
        {
            throw new ArgumentException("El documento no puede ser nulo.");
        }

        /*if (archivo == null)
        {
            throw new ArgumentException("El archivo no puede estar vacío.");
        }*/

        if (Path.GetExtension(archivo.FileName).ToLower() != ".pdf")
        {
            throw new ArgumentException("Solo se permiten archivos PDF.");
        }
        
        // Guardar el archivo en la carpeta especificada
        var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "uploads");
        if (!Directory.Exists(uploadsFolder))
            Directory.CreateDirectory(uploadsFolder);
        
        var fileName = Path.GetFileNameWithoutExtension(archivo.FileName)
            .Replace(" ", "_")
            .Replace("á", "a")
            .Replace("é", "e")
            .Replace("í", "i")
            .Replace("ó", "o")
            .Replace("ú", "u")+ Path.GetExtension(archivo.FileName);
        
        var filePath = Path.Combine(uploadsFolder, fileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            archivo.CopyTo(stream);
        }
        var proyecto = new Proyecto
        {
            Titulo = proyectoDTO.Titulo,
            Resumen = proyectoDTO.Resumen,
            EnlaceRepositorio = proyectoDTO.EnlaceRepositorio,
            DocumentoPdf = $"/uploads/{fileName}",
            Estado = proyectoDTO.Estado,
            FechaSubida = DateTime.Now, // Asignar fecha actual
            EstatusAprobacion = "Pendiente" // Por defecto
        };

        _context.Proyectos.Add(proyecto);
        _context.SaveChanges();
        return proyecto;
    }

    public void ActualizarProyecto(ProyectoActualizarDTO proyectoDTO)
    {
        var existente = _context.Proyectos.Find(proyectoDTO.Idproyecto);
        if (existente == null) throw new Exception("Proyecto no encontrado");

        existente.Titulo = proyectoDTO.Titulo;
        existente.Resumen = proyectoDTO.Resumen;
        existente.EnlaceRepositorio = proyectoDTO.EnlaceRepositorio;
        existente.DocumentoPdf = proyectoDTO.DocumentoPdf;
        existente.Estado = proyectoDTO.Estado;
        existente.EstatusAprobacion = proyectoDTO.EstatusAprobacion;

        _context.SaveChanges();
    }

    public void EliminarProyecto(int id)
    {
        var proyecto = _context.Proyectos.Find(id);
        if (proyecto == null) throw new Exception("Proyecto no encontrado");

        _context.Proyectos.Remove(proyecto);
        _context.SaveChanges();
    }

    public IEnumerable<Proyecto> BuscarProyectos(string terminoBusqueda)
    {
        if (string.IsNullOrEmpty(terminoBusqueda))
        {
            return _context.Proyectos.ToList(); // Si no se proporciona un término, devuelve todos los proyectos
        }

        terminoBusqueda = terminoBusqueda.ToLower(); // Convertir a minúsculas para búsqueda insensible a mayúsculas/minúsculas

        return _context.Proyectos
            .Where(p => p.Titulo.ToLower().Contains(terminoBusqueda) || 
                        p.Resumen.ToLower().Contains(terminoBusqueda) &&
                        p.EstatusAprobacion.ToLower() == "aprobado")
             .ToList();
    }
}