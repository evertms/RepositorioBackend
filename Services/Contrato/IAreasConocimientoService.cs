using ProyectoFinal.Models;

namespace ProyectoFinal.Services.Contrato;

public interface IAreasConocimientoService
{
    public IEnumerable<AreasConocimiento> ObtenerTodas();
}