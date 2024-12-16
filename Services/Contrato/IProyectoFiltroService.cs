using ProyectoFinal.Models;
using ProyectoFinal.Models.DTO;

namespace ProyectoFinal.Services.Contrato;

public interface IProyectoFiltroService
{
    IEnumerable<Proyecto> FiltrarProyectos(ProyectoFiltroDTO filtro);
}