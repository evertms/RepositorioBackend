using ProyectoFinal.Context;
using ProyectoFinal.Models;
using ProyectoFinal.Services.Contrato;

namespace ProyectoFinal.Services.Implementacion;

public class AreasConocimientoService : IAreasConocimientoService
{
    private readonly AppDbContext _context;

    public AreasConocimientoService(AppDbContext context)
    {
        _context = context;
    }

    public IEnumerable<AreasConocimiento> ObtenerTodas()
    {
        return _context.AreasConocimientos.ToList();
    }
}