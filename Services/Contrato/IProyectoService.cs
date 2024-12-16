using System.Runtime.InteropServices.ComTypes;
using ProyectoFinal.Models;
using ProyectoFinal.Models.DTO;

namespace ProyectoFinal.Services.Contrato;

public interface IProyectoService
{
    IEnumerable<Proyecto> ObtenerTodosAprobados();
    Proyecto ObtenerPorId(int id);
    Proyecto CrearProyectoConArchivo(ProyectoCrearDTO proyecto, IFormFile archivo);
    void ActualizarProyecto(ProyectoActualizarDTO proyecto);
    void EliminarProyecto(int id);
    IEnumerable<Proyecto> BuscarProyectos(string terminoBusqueda);
    IEnumerable<Proyecto> ObtenerProyectosPorUsuario(int idUsuario);
    IEnumerable<Proyecto> ObtenerProyectosRecientes();
}