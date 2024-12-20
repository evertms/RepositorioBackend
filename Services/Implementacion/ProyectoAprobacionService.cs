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

        if (aprobacionDTO.RolAprobador == "Revisor")
        {
            if (aprobacionDTO.EstatusAprobacion == "Aprobado")
            {
                proyecto.EstatusAprobacion = "Parcialmente aprobado";
            }
            else
            {
                proyecto.EstatusAprobacion = aprobacionDTO.EstatusAprobacion;
            }
        }
        
        else if (aprobacionDTO.RolAprobador == "Administrador")
        {
            if (aprobacionDTO.EstatusAprobacion == "Aprobado")
            {
                proyecto.EstatusAprobacion = "Aprobado";
            }
            else
            {
                proyecto.EstatusAprobacion = "Pendiente";
            }
        }

        proyecto.ComentarioAprobacion = aprobacionDTO.ComentariosAprobacion;
        _context.SaveChanges();
    }

    public IEnumerable<Proyecto> ObtenerProyectosPendientes()
    {
        return _context.Proyectos.Where(p => p.EstatusAprobacion == "Pendiente").ToList();
    }

    public IEnumerable<Proyecto> ObtenerProyectosParcialmenteAprobados()
    {
        var projs = _context.Proyectos
            .Where(p => p.EstatusAprobacion.ToLower() == "Parcialmente aprobado")
            .ToList();
        return projs;
    }
}