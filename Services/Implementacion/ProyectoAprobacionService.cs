using ProyectoFinal.Context;
using ProyectoFinal.Models;
using ProyectoFinal.Models.DTO;
using ProyectoFinal.Services.Contrato;

namespace ProyectoFinal.Services.Implementacion;

public class ProyectoAprobacionService : IProyectoAprobacionService
{
    private readonly AppDbContext _context;

    public ProyectoAprobacionService(AppDbContext context)
    {
        _context = context;
    }

    public void AprobarProyecto(ProyectoAprobacionDTO aprobacionDTO)
    {
        var proyecto = _context.Proyectos.Find(aprobacionDTO.IdProyecto);
        if (proyecto == null)
        {
            throw new Exception("Proyecto no encontrado.");
        }

        if (proyecto.EstatusAprobacion != "Pendiente")
        {
            throw new Exception("El proyecto ya fue revisado.");
        }

        proyecto.EstatusAprobacion = aprobacionDTO.EstatusAprobacion;
        proyecto.ComentarioAprobacion = aprobacionDTO.ComentariosAprobacion;
        proyecto.Idadministrador = aprobacionDTO.IDAdministrador;

        _context.SaveChanges();
    }

    public IEnumerable<Proyecto> ObtenerProyectosPendientes()
    {
        return _context.Proyectos.Where(p => p.EstatusAprobacion == "Pendiente").ToList();
    }
}