using ProyectoFinal.Models;
using ProyectoFinal.Models.DTO;

namespace ProyectoFinal.Services;

public interface IProyectoFiltroService
{
    IEnumerable<Proyecto> FiltrarProyectos(ProyectoFiltroDTO filtro);
}