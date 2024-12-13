using ProyectoFinal.Models;

namespace ProyectoFinal.Services;

public interface ITiposTrabajoService
{
    IEnumerable<TiposTrabajo> GetTiposTrabajo();
}