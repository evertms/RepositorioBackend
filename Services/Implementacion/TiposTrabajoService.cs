using ProyectoFinal.Context;
using ProyectoFinal.Models;
using ProyectoFinal.Services.Contrato;

namespace ProyectoFinal.Services.Implementacion;

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