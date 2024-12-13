using ProyectoFinal.Context;
using ProyectoFinal.Models;

namespace ProyectoFinal.Services;


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