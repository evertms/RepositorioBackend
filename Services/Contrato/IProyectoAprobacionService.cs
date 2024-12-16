using ProyectoFinal.Models;
using ProyectoFinal.Models.DTO;

namespace ProyectoFinal.Services.Contrato;

public interface IProyectoAprobacionService
{
    void AprobarProyecto(ProyectoAprobacionDTO aprobacionDTO);
    IEnumerable<Proyecto> ObtenerProyectosPendientes();
    IEnumerable<Proyecto> ObtenerProyectosParcialmenteAprobados();
}