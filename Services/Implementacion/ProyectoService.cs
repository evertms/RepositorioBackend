using ProyectoFinal.Context;
using ProyectoFinal.Models;
using ProyectoFinal.Models.DTO;
using ProyectoFinal.Services.Contrato;

namespace ProyectoFinal.Services.Implementacion;

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

        if (archivo == null || archivo.Length == 0)
        {
            throw new ArgumentException("El archivo no puede estar vacío.");
        }

        if (Path.GetExtension(archivo.FileName).ToLower() != ".pdf")
        {
            throw new ArgumentException("Solo se permiten archivos PDF.");
        }

        Console.WriteLine(Directory.GetCurrentDirectory());
        var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "uploads");
        if (!Directory.Exists(uploadsFolder))
            Directory.CreateDirectory(uploadsFolder);

        var fileNameParcial = Path.GetFileNameWithoutExtension(archivo.FileName)
            .Replace(" ", "_")
            .Replace("á", "a")
            .Replace("é", "e")
            .Replace("í", "i")
            .Replace("ó", "o")
            .Replace("ú", "u");
        var extension = Path.GetExtension(archivo.FileName);
        var fileName = $"{fileNameParcial}_{DateTime.Now:yyyyMMddHHmmss}{extension}";
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
            Idtipo =  proyectoDTO.IdTipoTrabajo,
            FechaSubida = DateTime.Now, // Asignar fecha actual
            EstatusAprobacion = "Pendiente", // Por defecto
            Idusuario = proyectoDTO.IdUsuario
        };

        foreach (var participanteDto in proyectoDTO.Participantes)
        {
            var participanteExistente = _context.Participantes
                .FirstOrDefault(p => p.NombreCompleto.ToLower() == participanteDto.NombreCompleto.Trim().ToLower() 
                                     && p.Carrera.ToLower() == participanteDto.Carrera.Trim().ToLower());
            if (participanteExistente != null)
            {
                proyecto.Idparticipantes.Add(participanteExistente);
                if (!participanteExistente.Idproyectos.Contains(proyecto))
                {
                    participanteExistente.Idproyectos.Add(proyecto);
                }
            }
            else
            {
                var nuevoParticipante = new Participante
                {
                    NombreCompleto = participanteDto.NombreCompleto.Trim(),
                    Carrera = participanteDto.Carrera.Trim()
                };
                nuevoParticipante.Idproyectos.Add(proyecto);
                proyecto.Idparticipantes.Add(nuevoParticipante);
                _context.Add(nuevoParticipante);
            }
        }

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
            return _context.Proyectos
                .Where(p => p.EstatusAprobacion.ToLower() == "aprobado")
                .ToList(); // Si no se proporciona un término, devuelve todos los proyectos
        }
        terminoBusqueda = terminoBusqueda.ToLower(); // Convertir a minúsculas para búsqueda insensible a mayúsculas/minúsculas

        return _context.Proyectos
            .Where(p => p.Titulo.ToLower().Contains(terminoBusqueda) || 
                        p.Resumen.ToLower().Contains(terminoBusqueda) &&
                        p.EstatusAprobacion.ToLower() == "aprobado")
             .ToList();
    }
    public IEnumerable<Proyecto> ObtenerProyectosPorUsuario(int idUsuario)
    {
        var proyectos = _context.Proyectos.Where(
            p=> p.Idusuario == idUsuario).ToList();
        return proyectos;
    }

    public IEnumerable<Proyecto> ObtenerProyectosRecientes()
    {
        var proyectos = _context.Proyectos.Where(
            p => p.EstatusAprobacion.ToLower() == "aprobado")
            .OrderByDescending(p => p.FechaSubida)
            .ToList();
        return proyectos;
    }
}