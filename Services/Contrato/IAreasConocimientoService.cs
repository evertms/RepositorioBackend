using ProyectoFinal.Models;

namespace ProyectoFinal.Services;

public interface IAreasConocimientoService
{
    public IEnumerable<AreasConocimiento> ObtenerTodas();
}