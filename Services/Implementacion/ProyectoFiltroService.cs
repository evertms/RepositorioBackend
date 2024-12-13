using Microsoft.EntityFrameworkCore;
using ProyectoFinal.Context;
using ProyectoFinal.Models;
using ProyectoFinal.Models.DTO;

namespace ProyectoFinal.Services;

public class ProyectoFiltroService : IProyectoFiltroService
{
    private readonly AppDbContext _context;

    public ProyectoFiltroService(AppDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Proyecto> FiltrarProyectos(ProyectoFiltroDTO filtro)
    {
        var query = _context.Proyectos
            .Include(p => p.Idareas) // Incluye áreas de conocimiento
            .Include(p => p.Idconceptos) // Incluye conceptos
            .Include(p => p.Idparticipantes) // Incluye participantes
            .AsQueryable();

        // Filtro por estado
        if (!string.IsNullOrEmpty(filtro.Estado))
        {
            query = query.Where(p => p.Estado == filtro.Estado);
        }

        // Filtro por estatus de aprobación
        if (!string.IsNullOrEmpty(filtro.EstatusAprobacion))
        {
            query = query.Where(p => p.EstatusAprobacion == filtro.EstatusAprobacion);
        }

        // Filtro por áreas de conocimiento
        if (filtro.IDAreas != null && filtro.IDAreas.Any())
        {
            query = query.Where(p => p.Idareas.Any(a => filtro.IDAreas.Contains(a.Idarea)));
        }

        // Filtro por conceptos
        if (filtro.IDConceptos != null && filtro.IDConceptos.Any())
        {
            query = query.Where(p => p.Idconceptos.Any(c => filtro.IDConceptos.Contains(c.Idconcepto)));
        }

        // Filtro por rango de fechas
        if (filtro.FechaInicio.HasValue)
        {
            query = query.Where(p => p.FechaSubida >= filtro.FechaInicio.Value);
        }

        if (filtro.FechaFin.HasValue)
        {
            query = query.Where(p => p.FechaSubida <= filtro.FechaFin.Value);
        }

        return query.ToList();
    }
}