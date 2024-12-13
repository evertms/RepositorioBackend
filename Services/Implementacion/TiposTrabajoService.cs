using ProyectoFinal.Context;
using ProyectoFinal.Models;

namespace ProyectoFinal.Services;

public class TiposTrabajoService : ITiposTrabajoService
{
    private readonly AppDbContext _context;

    public TiposTrabajoService(AppDbContext context)
    {
        _context = context;
    }
    public IEnumerable<TiposTrabajo> GetTiposTrabajo()
    {
        return _context.TiposTrabajos.ToList();
    }
}