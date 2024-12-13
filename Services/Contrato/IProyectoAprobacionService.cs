using ProyectoFinal.Models;
using ProyectoFinal.Models.DTO;

namespace ProyectoFinal.Services;

public interface IProyectoAprobacionService
{
    void AprobarProyecto(ProyectoAprobacionDTO aprobacionDTO);
    IEnumerable<Proyecto> ObtenerProyectosPendientes();
}