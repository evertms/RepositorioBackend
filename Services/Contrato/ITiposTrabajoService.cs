using ProyectoFinal.Models;

namespace ProyectoFinal.Services.Contrato;

public interface ITiposTrabajoService
{
    IEnumerable<TiposTrabajo> GetTiposTrabajo();
}